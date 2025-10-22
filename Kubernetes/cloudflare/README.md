# Cloudflare DNS Ayarlarý Rehberi

## Tunnel ID
```
70111fbb-abb5-4592-9d16-e6eefda961bc
```

## Cloudflare Dashboard'da Yapýlacak Ýþlemler

### 1. DNS Kayýtlarý Oluþturma

Cloudflare Dashboard > DNS > Records bölümüne gidin ve aþaðýdaki CNAME kayýtlarýný ekleyin:

| Type  | Name  | Target (Content)         | Proxy Status | TTL  |
|-------|-------------|----------------------------------------------------------|--------------|------|
| CNAME | ui          | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |
| CNAME | bff         | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |
| CNAME | definitions | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |
| CNAME | production  | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |
| CNAME | identity    | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |
| CNAME | seq         | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |
| CNAME | @           | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied      | Auto |

**NOT:** @ iþareti root domain'i (b-online-store.com) temsil eder.

### 2. Wildcard Subdomain (Opsiyonel)
Gelecekte yeni subdomain'ler eklemek için wildcard kaydý:

| Type  | Name | Target (Content) | Proxy Status | TTL  |
|-------|------|----------------------------------------------------------|--------------|------|
| CNAME | * | 70111fbb-abb5-4592-9d16-e6eefda961bc.cfargotunnel.com   | Proxied | Auto |

### 3. SSL/TLS Ayarlarý

1. **SSL/TLS** > **Overview** bölümüne gidin
2. Encryption mode'u **"Full"** veya **"Full (strict)"** olarak ayarlayýn
3. **Edge Certificates** bölümünde:
   - Always Use HTTPS: **ON**
   - Automatic HTTPS Rewrites: **ON**
   - Minimum TLS Version: **TLS 1.2** veya üstü

### 4. Tunnel Yapýlandýrmasý Kontrolü

1. **Zero Trust** > **Networks** > **Tunnels** bölümüne gidin
2. Tunnel'ýnýzý bulun (ID: 70111fbb-abb5-4592-9d16-e6eefda961bc)
3. Status'un **"Healthy"** olduðunu kontrol edin
4. **Public Hostnames** tabýnda tüm hostname'lerin eklendiðini doðrulayýn

## Ubuntu Sunucuda Yapýlacak Ýþlemler

### 1. Script'i Çalýþtýrma

```bash
# Script'e çalýþtýrma izni ver
chmod +x setup-cloudflared.sh

# Script'i çalýþtýr
sudo ./setup-cloudflared.sh
```

### 2. Service Durumu Kontrolü

```bash
# Service durumunu kontrol et
sudo systemctl status cloudflared

# Loglarý görüntüle
sudo journalctl -u cloudflared -f
```

### 3. Test Ýþlemleri

```bash
# Her bir servisin eriþilebilirliðini test et
curl -I https://ui.b-online-store.com
curl -I https://bff.b-online-store.com
curl -I https://definitions.b-online-store.com
curl -I https://production.b-online-store.com
curl -I https://identity.b-online-store.com
curl -I https://seq.b-online-store.com

# Ana domain testi
curl -I https://b-online-store.com
```

## Sorun Giderme

### Tunnel Baðlanamýyor

1. Credentials dosyasýnýn doðru konumda olduðunu kontrol edin:
   ```bash
   ls -la /etc/cloudflared/70111fbb-abb5-4592-9d16-e6eefda961bc.json
   ```

2. Config dosyasýný doðrulayýn:
   ```bash
   sudo cloudflared tunnel --config /etc/cloudflared/config.yml ingress validate
   ```

3. Service loglarýný inceleyin:
   ```bash
   sudo journalctl -u cloudflared -n 100 --no-pager
   ```

### DNS Çözümleme Sorunu

1. DNS propagation'ý kontrol edin (24-48 saat sürebilir):
   ```bash
   nslookup ui.b-online-store.com
   dig ui.b-online-store.com
   ```

2. Cloudflare DNS cache'i temizleyin:
   - Cloudflare Dashboard > Caching > Configuration > Purge Everything

### SSL Hatasý

1. Nginx Ingress Controller'ýn TLS sertifikasýný kontrol edin:
   ```bash
   kubectl get secret bonlinestore-com-certificate -o yaml
   ```

2. Cloudflare SSL/TLS mode'un "Full" olduðundan emin olun

### 502 Bad Gateway

1. Kubernetes servislerin çalýþtýðýný kontrol edin:
   ```bash
   kubectl get pods -A
   kubectl get svc -A
   ```

2. Nginx Ingress Controller loglarýný inceleyin:
   ```bash
   kubectl logs -n ingress-nginx -l app.kubernetes.io/name=ingress-nginx
   ```

## Test Checklist

- [ ] Cloudflared service çalýþýyor
- [ ] Tüm DNS kayýtlarý oluþturuldu
- [ ] SSL/TLS ayarlarý yapýldý
- [ ] Tunnel status "Healthy"
- [ ] https://ui.b-online-store.com eriþilebilir
- [ ] https://bff.b-online-store.com eriþilebilir
- [ ] https://definitions.b-online-store.com eriþilebilir
- [ ] https://production.b-online-store.com eriþilebilir
- [ ] https://identity.b-online-store.com eriþilebilir
- [ ] https://seq.b-online-store.com eriþilebilir
- [ ] https://b-online-store.com eriþilebilir

## Önemli Notlar

1. **Cloudflare Proxy**: Tüm DNS kayýtlarý "Proxied" (turuncu bulut) durumunda olmalý
2. **TLS Termination**: Cloudflare SSL/TLS þifrelemesini sonlandýrýr, Kubernetes'e HTTPS ile baðlanýr
3. **originRequest noTLSVerify**: Self-signed sertifika kullanýyorsanýz true olmalý
4. **Performance**: Cloudflare CDN otomatik olarak içerikleri cache'ler
5. **Security**: Cloudflare WAF ve DDoS protection otomatik olarak aktiftir

## Geliþmiþ Yapýlandýrma (Opsiyonel)

### Access Policies Ekleme
Belirli servislere eriþimi kýsýtlamak için:
1. **Zero Trust** > **Access** > **Applications**
2. **Add an application** týklayýn
3. Self-hosted seçin ve hostname'i belirtin

### Load Balancing
Yüksek trafik için:
1. **Traffic** > **Load Balancing**
2. Origin pool oluþturun
3. Cloudflare Tunnel'larý origin olarak ekleyin

### Caching Rules
Performans optimizasyonu için:
1. **Caching** > **Configuration** > **Cache Rules**
2. Path pattern'e göre cache kurallarý ekleyin
