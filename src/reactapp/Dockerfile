FROM node:lts-buster-slim as builder
WORKDIR /build
COPY src/reactapp/package.json .
RUN npm install
COPY src/reactapp/ .
RUN npm run build

FROM nginx:latest
WORKDIR /app

COPY src/reactapp/nginx/https.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /build/build /app

## Add the wait script to the image
ADD src/wait /app/wait
RUN chmod +x /app/wait

CMD /app/wait && exec nginx -g 'daemon off;'
