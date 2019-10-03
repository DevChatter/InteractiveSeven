'use strict'

const glob = require('glob')
const pages = {}

glob.sync('./src/pages/**/app.js').forEach(path => {
  const chunk = path.split('./src/pages/')[1].split('/app.js')[0]
  pages[chunk] = {
    entry: path,
    template: 'public/index.html',
    title: '',
    chunks: ['chunk-vendors', 'chunk-common', chunk]
  }
})
module.exports = {
  publicPath: '.',
  pages,
  devServer: { https: true }
}
