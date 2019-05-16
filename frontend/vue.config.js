module.exports = {
  devServer: {
    proxy: {
      '^/api': {
        target: 'https://remote.palholmen.se',
        changeOrigin: true,
      },
    }
  },
};
