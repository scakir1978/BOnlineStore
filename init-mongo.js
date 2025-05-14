print("Script calismaya basladi.")

db = db.getSiblingDB('admin');

db.createUser(
    {
        user: "admin",
        pwd: "Scag18548912345*",
        roles: [{ role: "root", db: "admin" }]
    }
);

print("Admin kullanicisi basariyla olusturuldu: admin");
