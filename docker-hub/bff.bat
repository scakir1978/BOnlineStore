docker system prune -f
docker-compose build bff.bonlinestore.com
docker push scakir1978/bff.bonlinestore.com:latest