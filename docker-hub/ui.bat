docker system prune -f
docker-compose build ui.bonlinestore.com --no-cache
docker push scakir1978/ui.bonlinestore.com:latest
