#!/usr/bin/env bash
IMAGE_NAME="definitions.bonlinestore.com"
REPOSITORY_TAG=${IMAGE_NAME}":latest"
DOCKER_REPO_ADDRESS="scakir1978"
#docker system prune -f
docker build --rm --pull -t ${REPOSITORY_TAG} .
docker tag ${REPOSITORY_TAG} ${DOCKER_REPO_ADDRESS}/${REPOSITORY_TAG}
docker login ${DOCKER_REPO_ADDRESS} -u scakir1978 -p Scag185489
docker push ${DOCKER_REPO_ADDRESS}/${REPOSITORY_TAG}
docker rmi ${REPOSITORY_TAG}
docker rmi ${DOCKER_REPO_ADDRESS}/${REPOSITORY_TAG}
read -p "Press enter to continue"