(function (window) {
  window.env = window.env || {};

  // Environment variables
  window['env']['GRAPHQL_URL'] = '${GRAPHQL_URL}';
})(this);
