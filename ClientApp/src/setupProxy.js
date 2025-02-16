const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function (app) {
    app.use(
        '/api',
        createProxyMiddleware({
            target: 'http://localhost:16225',
            changeOrigin: true,
            secure: false // if your target uses self-signed certificates
        })
    );
};