# SSL sertificate

Use the ```server.crt``` and ```server.key``` that is already generated


# If you need to create a self signed certificate again:
- Adjust certificate.cnf file
- ```openssl req -new -x509 -newkey rsa:4096 -sha512 -nodes -keyout server.key -days 356 -out server.crt -config certificate.cnf```
- ```openssl pkcs12 -export -out server.p12 -inkey server.key -in server.crt```
- ```openssl pkcs12 -export -out server.pfx -inkey server.key -in server.crt```