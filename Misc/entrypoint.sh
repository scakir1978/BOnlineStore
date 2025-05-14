#!/bin/bash
set -e

# MongoDB sunucusunu arka planda ba�lat
/usr/local/bin/docker-entrypoint.sh mongod --fork --logpath /var/log/mongodb/mongod.log

# Bir s�re bekle (MongoDB'nin tamamen ba�lamas� i�in)
sleep 5

echo "mongosh ile init scripti �al��t�r�l�yor..."
mongosh --eval "load('/docker-entrypoint-initdb.d/init-mongo.js')"

# MongoDB sunucusunu �n planda tut (iste�e ba�l�, konteynerin �al��maya devam etmesi i�in)
/usr/local/bin/docker-entrypoint.sh "$@"