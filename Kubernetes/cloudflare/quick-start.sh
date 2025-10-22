#!/bin/bash

# Renkler
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

clear
echo -e "${BLUE}╔════════════════════════════════════════════════╗${NC}"
echo -e "${BLUE}║   Cloudflare Tunnel Quick Start               ║${NC}"
echo -e "${BLUE}║   b-online-store.com          ║${NC}"
echo -e "${BLUE}╚════════════════════════════════════════════════╝${NC}"
echo ""

# 1. Kubernetes cluster durumu
echo -e "${YELLOW}1. Kubernetes Cluster Durumu Kontrol Ediliyor...${NC}"
if kubectl cluster-info &> /dev/null; then
    echo -e "${GREEN}✓ Kubernetes cluster erişilebilir${NC}"
    kubectl get nodes
else
    echo -e "${RED}✗ Kubernetes cluster'a erişilemiyor!${NC}"
    exit 1
fi
echo ""

# 2. Ingress durumu
echo -e "${YELLOW}2. Ingress Controller Durumu Kontrol Ediliyor...${NC}"
INGRESS_POD=$(kubectl get pods -n ingress-nginx -l app.kubernetes.io/name=ingress-nginx -o jsonpath='{.items[0].metadata.name}' 2>/dev/null)
if [ -n "$INGRESS_POD" ]; then
    echo -e "${GREEN}✓ Ingress Controller çalışıyor: $INGRESS_POD${NC}"
else
  echo -e "${RED}✗ Ingress Controller bulunamadı!${NC}"
    exit 1
fi
echo ""

# 3. Servisler durumu
echo -e "${YELLOW}3. Kubernetes Servisleri Kontrol Ediliyor...${NC}"
SERVICES=("ui-bonlinestore-com" "bff-bonlinestore-com" "definitions-bonlinestore-com" "production-bonlinestore-com" "identity-bonlinestore-com" "seq")
ALL_SERVICES_OK=true

for SERVICE in "${SERVICES[@]}"; do
  if kubectl get svc "$SERVICE" &> /dev/null; then
     echo -e "${GREEN}✓ $SERVICE${NC}"
    else
        echo -e "${RED}✗ $SERVICE bulunamadı!${NC}"
      ALL_SERVICES_OK=false
    fi
done
echo ""

if [ "$ALL_SERVICES_OK" = false ]; then
    echo -e "${RED}Bazı servisler eksik! Devam etmeden önce servisleri oluşturun.${NC}"
    exit 1
fi

# 4. Ingress routing kontrolü
echo -e "${YELLOW}4. Ingress Routing Kontrol Ediliyor...${NC}"
if kubectl get ingress bonlinestore-routing &> /dev/null; then
    echo -e "${GREEN}✓ Ingress routing yapılandırılmış${NC}"
kubectl get ingress bonlinestore-routing
else
    echo -e "${RED}✗ Ingress routing bulunamadı!${NC}"
    exit 1
fi
echo ""

# 5. TLS sertifika kontrolü
echo -e "${YELLOW}5. TLS Sertifikası Kontrol Ediliyor...${NC}"
if kubectl get secret bonlinestore-com-certificate &> /dev/null; then
    echo -e "${GREEN}✓ TLS sertifikası mevcut${NC}"
else
    echo -e "${RED}✗ TLS sertifikası bulunamadı!${NC}"
    exit 1
fi
echo ""

# 6. Cloudflared kurulum
echo -e "${YELLOW}6. Cloudflared Kurulum${NC}"
read -p "Cloudflared'i kurmak ve yapılandırmak istiyor musunuz? (y/n): " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]; then
    if [ -f "./setup-cloudflared.sh" ]; then
        chmod +x ./setup-cloudflared.sh
      sudo ./setup-cloudflared.sh
    else
        echo -e "${RED}setup-cloudflared.sh bulunamadı!${NC}"
        exit 1
    fi
else
    echo -e "${YELLOW}Cloudflared kurulumu atlandı.${NC}"
fi
echo ""

# 7. Test
echo -e "${YELLOW}7. Servisleri Test Et${NC}"
read -p "Servisleri test etmek istiyor musunuz? (y/n): " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo -e "${BLUE}Not: DNS propagation'ı beklemeniz gerekebilir (5-10 dakika)${NC}"
    echo ""
    
    HOSTNAMES=("ui.b-online-store.com" "bff.b-online-store.com" "definitions.b-online-store.com" "production.b-online-store.com" "identity.b-online-store.com" "seq.b-online-store.com" "b-online-store.com")
    
    for HOSTNAME in "${HOSTNAMES[@]}"; do
        echo -e "${YELLOW}Testing: $HOSTNAME${NC}"
        HTTP_CODE=$(curl -s -o /dev/null -w "%{http_code}" -k "https://$HOSTNAME" --max-time 10)
        
        if [ "$HTTP_CODE" == "200" ] || [ "$HTTP_CODE" == "302" ] || [ "$HTTP_CODE" == "301" ]; then
   echo -e "${GREEN}✓ $HOSTNAME (HTTP $HTTP_CODE)${NC}"
        else
            echo -e "${RED}✗ $HOSTNAME (HTTP $HTTP_CODE)${NC}"
        fi
    done
fi
echo ""

# 8. Özet
echo -e "${BLUE}╔════════════════════════════════════════════════╗${NC}"
echo -e "${BLUE}║   Kurulum Tamamlandı!            ║${NC}"
echo -e "${BLUE}╚════════════════════════════════════════════════╝${NC}"
echo ""
echo -e "${GREEN}Sıradaki Adımlar:${NC}"
echo ""
echo "1. Cloudflare Dashboard'a gidin:"
echo "   https://dash.cloudflare.com/"
echo ""
echo "2. DNS kayıtlarını ekleyin (README.md'de detaylar var)"
echo ""
echo "3. SSL/TLS ayarlarını 'Full' veya 'Full (strict)' yapın"
echo ""
echo "4. Tunnel durumunu kontrol edin:"
echo "sudo systemctl status cloudflared"
echo ""
echo "5. Logları takip edin:"
echo "   sudo journalctl -u cloudflared -f"
echo ""
echo -e "${YELLOW}Faydalı Komutlar:${NC}"
echo "  kubectl get pods -A      # Tüm pod'ları listele"
echo "  kubectl get svc -A      # Tüm servisleri listele"
echo "  kubectl get ingress # Ingress'leri listele"
echo "  kubectl logs -n ingress-nginx <pod>    # Ingress logları"
echo "  sudo systemctl restart cloudflared     # Cloudflared'i yeniden başlat"
echo ""
