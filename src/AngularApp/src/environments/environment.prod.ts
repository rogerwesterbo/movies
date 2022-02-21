declare let require: any;
declare let window: any;
// eslint-disable-next-line @typescript-eslint/no-var-requires
const { version: appVersion } = require('../../package.json');

export const environment = {
  appVersion,
  graphqlUrl: window['env']['GRAPHQL_URL'] || null,
  production: true
};
