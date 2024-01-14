FROM node:18.15.0 AS build
WORKDIR /dist/src/app

# Copy files to virtual directory
COPY package.json /dist/src/app/package.json
COPY package-lock.json /dist/src/app/package-lock.json

# Run command in Virtual directory
RUN npm cache clean --force
# Copy files from local machine to virtual directory in docker image
COPY . .
RUN npm install --legacy-peer-deps
RUN npm run build-prod


### STAGE 2:RUN ###
# Defining nginx image to be used
FROM nginx:latest
# Copying compiled code and nginx config to different folder
# NOTE: This path may change according to your project's output folder 
COPY --from=build /dist/src/app/dist/velzon /usr/share/nginx/html
#COPY /dist/velzon /usr/share/nginx/html
COPY ssl/bonlinestore.crt  /etc/nginx/certs/bonlinestore.crt
COPY ssl/bonlinetore-decrypted.key  /etc/nginx/certs/bonlinestore.key
COPY nginx.conf  /etc/nginx/conf.d/default.conf
# Exposing a port, here it means that inside the container 
# the app will be using Port 80 while running
EXPOSE 80
EXPOSE 443
