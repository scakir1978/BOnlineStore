FROM node:18.20.5 AS build
WORKDIR /dist/src/app

# Set Node.js memory options for memory-constrained environments
ENV NODE_OPTIONS="--max_old_space_size=3072"

# Copy package files first for better caching
COPY package.json package-lock.json /dist/src/app/

# Clean npm cache and install dependencies with optimizations
RUN npm cache clean --force && \
    npm ci --only=production --no-audit --no-fund && \
    npm cache clean --force

# Copy source files
COPY . .

# Build with memory optimizations and production settings
RUN npm run build-prod -- --optimization=true --build-optimizer=true --aot=true --source-map=false


### STAGE 2:RUN ###
# Defining nginx image to be used
FROM nginx:latest
# Copying compiled code and nginx config to different folder
# NOTE: This path may change according to your project's output folder 
COPY --from=build /dist/src/app/dist/velzon /usr/share/nginx/html
#COPY /dist/velzon /usr/share/nginx/html
COPY ssl/bonlinestore.crt  /etc/nginx/certs/bonlinestore.crt
COPY ssl/bonlinestore-decrypted.key  /etc/nginx/certs/bonlinestore.key
COPY nginx.conf  /etc/nginx/conf.d/default.conf
# Exposing a port, here it means that inside the container 
# the app will be using Port 80 while running
EXPOSE 80
EXPOSE 443
