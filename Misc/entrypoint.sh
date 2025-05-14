#!/bin/bash
set -e

# MongoDB sunucusunu arka planda baþlat
/usr/local/bin/docker-entrypoint.sh mongod --fork --logpath /var/log/mongodb/mongod.log

# Bir süre bekle (MongoDB'nin tamamen baþlamasý için)
sleep 5

echo "mongosh ile init scripti çalýþtýrýlýyor..."
mongosh --eval "load('/docker-entrypoint-initdb.d/init-mongo.js')"

# MongoDB sunucusunu ön planda tut (isteðe baðlý, konteynerin çalýþmaya devam etmesi için)
/usr/local/bin/docker-entrypoint.sh "$@"