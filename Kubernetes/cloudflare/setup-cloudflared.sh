#!/bin/bash

echo "=== Cloudflare Tunnel Setup Script ==="
echo "Tunnel ID: 70111fbb-abb5-4592-9d16-e6eefda961bc"
echo "========================================="

# Renk kodlarý
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# 1. Cloudflared kurulu mu kontrol et
echo -e "${YELLOW}1. Cloudflared kontrol ediliyor...${NC}"
if ! command -v cloudflared &> /dev/null; then
    echo -e "${RED}Cloudflared kurulu deðil. Kuruluyor...${NC}"
    
    # Cloudflared'i indir ve kur
    wget -q https://github.com/cloudflare/cloudflared/releases/latest/download/cloudflared-linux-amd64.deb
    sudo dpkg -i cloudflared-linux-amd64.deb
    rm cloudflared-linux-amd64.deb
    
    echo -e "${GREEN}Cloudflared kuruldu.${NC}"
else
    echo -e "${GREEN}Cloudflared zaten kurulu.${NC}"
    cloudflared --version
fi

# 2. Config dizinini oluþtur
echo -e "${YELLOW}2. Config dizini oluþturuluyor...${NC}"
sudo mkdir -p /etc/cloudflared
sudo chmod 755 /etc/cloudflared

# 3. Config dosyasýný kopyala
echo -e "${YELLOW}3. Config dosyasý kopyalanýyor...${NC}"
if [ -f "./config.yml" ]; then
    sudo cp ./config.yml /etc/cloudflared/config.yml
    sudo chmod 644 /etc/cloudflared/config.yml
    echo -e "${GREEN}Config dosyasý kopyalandý.${NC}"
else
    echo -e "${RED}HATA: config.yml dosyasý bulunamadý!${NC}"
    echo "Lütfen config.yml dosyasýný bu script ile ayný dizinde oluþturun."
    exit 1
fi

# 4. Credentials dosyasýný kontrol et
echo -e "${YELLOW}4. Credentials dosyasý kontrol ediliyor...${NC}"
CRED_FILE="/etc/cloudflared/70111fbb-abb5-4592-9d16-e6eefda961bc.json"
if [ ! -f "$CRED_FILE" ]; then
    echo -e "${RED}UYARI: Credentials dosyasý bulunamadý!${NC}"
    echo "Cloudflare Dashboard'dan credentials dosyasýný indirin ve þu konuma kopyalayýn:"
echo "$CRED_FILE"
    echo ""
    echo "veya þu komutu çalýþtýrýn:"
    echo "cloudflared tunnel login"
    read -p "Credentials dosyasý hazýr olduðunda devam etmek için ENTER'a basýn..."
else
    echo -e "${GREEN}Credentials dosyasý mevcut.${NC}"
fi

# 5. Config dosyasýný test et
echo -e "${YELLOW}5. Config dosyasý test ediliyor...${NC}"
sudo cloudflared tunnel --config /etc/cloudflared/config.yml ingress validate
if [ $? -eq 0 ]; then
    echo -e "${GREEN}Config dosyasý geçerli.${NC}"
else
    echo -e "${RED}Config dosyasýnda hata var!${NC}"
    exit 1
fi

# 6. Systemd service dosyasý oluþtur
echo -e "${YELLOW}6. Systemd service oluþturuluyor...${NC}"
sudo tee /etc/systemd/system/cloudflared.service > /dev/null <<EOF
[Unit]
Description=Cloudflare Tunnel
After=network.target

[Service]
Type=simple
User=root
ExecStart=/usr/local/bin/cloudflared tunnel --config /etc/cloudflared/config.yml run
Restart=on-failure
RestartSec=5s

[Install]
WantedBy=multi-user.target
EOF

echo -e "${GREEN}Systemd service dosyasý oluþturuldu.${NC}"

# 7. Service'i etkinleþtir ve baþlat
echo -e "${YELLOW}7. Cloudflared service etkinleþtiriliyor ve baþlatýlýyor...${NC}"
sudo systemctl daemon-reload
sudo systemctl enable cloudflared
sudo systemctl restart cloudflared

# 8. Service durumunu kontrol et
echo -e "${YELLOW}8. Service durumu kontrol ediliyor...${NC}"
sleep 3
sudo systemctl status cloudflared --no-pager

# 9. DNS kayýtlarýný kontrol et
echo -e "${YELLOW}9. DNS kayýtlarý kontrol ediliyor...${NC}"
echo "Cloudflare Dashboard'da aþaðýdaki CNAME kayýtlarýnýn olduðundan emin olun:"
echo ""
echo -e "${GREEN}Hostname${NC}    ${GREEN}Type${NC}    ${GREEN}Target${NC}"
echo "ui.b-online-store.com    CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "bff.b-online-store.com         CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "definitions.b-online-store.com CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "production.b-online-store.com  CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "identity.b-online-store.com    CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "seq.b-online-store.com         CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "b-online-store.com             CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo "*.b-online-store.com     CNAME   70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com"
echo ""

echo -e "${GREEN}=== Kurulum tamamlandý! ===${NC}"
echo ""
echo "Faydalý komutlar:"
echo "  sudo systemctl status cloudflared   - Service durumunu görüntüle"
echo "  sudo systemctl restart cloudflared  - Service'i yeniden baþlat"
echo "  sudo systemctl stop cloudflared     - Service'i durdur"
echo "  sudo journalctl -u cloudflared -f   - Loglarý canlý takip et"
echo ""
echo "Test için:"
echo "  curl -I https://ui.b-online-store.com"
