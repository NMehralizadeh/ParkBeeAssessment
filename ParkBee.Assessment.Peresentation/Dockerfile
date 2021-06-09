FROM node:14-alpine AS build

WORKDIR /src

COPY package.json .
COPY angular.json .

ENV NPM_CONFIG_LOGLEVEL warn

# RUN npm install
RUN npm install -g @angular/cli

COPY . .
RUN ng build --prod

FROM nginx:alpine
COPY --from=build /src/dist/ParkBee-Assessment-Peresentation /usr/share/nginx/html
EXPOSE 80
