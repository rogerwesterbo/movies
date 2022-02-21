FROM node:lts-buster-slim as builder
WORKDIR /build
COPY src/AngularApp/package.json .
RUN npm install
COPY src/AngularApp/ .
RUN npm run build --prod

FROM nginx:latest
WORKDIR /app

COPY src/AngularApp/nginx/https.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /build/dist/angular-app /app

## Add the wait script to the image
ADD src/wait /app/wait
RUN chmod +x /app/wait

CMD ["/bin/sh",  "-c",  "envsubst < /app/assets/env.template.js > /app/assets/env.js && /app/wait && exec nginx -g 'daemon off;'"]
