docker-compose build ui.bonlinestore.com --no-cache
docker tag 14f80b scakir1978/ui.bonlinestore.com:latest
rem docker login scakir1978 -u scakir1978 -p dckr_pat_mo1-R80Wj6J6VIb8j0Zx51iQNng
docker push scakir1978/ui.bonlinestore.com:latest
