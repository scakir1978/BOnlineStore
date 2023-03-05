docker-compose build identity.bonlinestore.com
docker tag ff1084 scakir1978/identity.bonlinestore.com:latest
rem docker login scakir1978 -u scakir1978 -p dckr_pat_mo1-R80Wj6J6VIb8j0Zx51iQNng
docker push scakir1978/identity.bonlinestore.com:latest