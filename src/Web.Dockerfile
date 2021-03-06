FROM node
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY /SpamReportsManagementSystem.Web/package.json /app/package.json
RUN npm install
RUN npm install @vue/cli@3.7.0 -g
CMD ["npm", "run", "serve"]