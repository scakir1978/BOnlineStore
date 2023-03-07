docker system prune -f
docker-compose build production.bonlinestore.com
docker push scakir1978/production.bonlinestore.com:latest