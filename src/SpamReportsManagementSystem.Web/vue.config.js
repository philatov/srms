const defaultProxy = {
  target: 'http://localhost:5000',
  onProxyRes: (response) => {
    const key = 'www-authenticate';
    response.headers[key] = response.headers[key] && response.headers[key].split(',');
  },
  logLevel: 'debug',
  ws: false,
  changeOrigin: true
};

module.exports = {
  lintOnSave: false,
  outputDir: '../wwwroot',
  assetsDir: 'static',
  runtimeCompiler: true,
  css: {
    sourceMap: true
  },
  devServer: {
    port: 5000,
    proxy: {
      '/api': defaultProxy
    }
  }
};
