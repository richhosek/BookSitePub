const path = require('path');

module.exports = {
    entry: './src/index.js',
    output: {
        library: 'TinymceVue',
        libraryTarget: "umd",
        path: path.resolve(__dirname, '../dist'),
        publicPath: '/dist/',
        filename: 'tinymce-vue.js'
    },
    module: {
        rules: [{
            test: /\.vue$/,
            loader: 'vue-loader',
            options: {
                loaders: {
                    'js': 'babel-loader',
                    'css': 'vue-style-loader!css-loader',
                    'scss': 'vue-style-loader!css-loader!sass-loader'
                }
            }
        }, {
            test: /\.js$/,
            loader: 'babel-loader',
            exclude: /node_modules/
        },   {
            test: /\.(png|jpe?g|gif|svg)(\?.*)?$/,
            loader: 'url-loader'
        },
            {
                test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
                loader: 'url-loader'
            }]
    },
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        },
        extensions: ['.vue','.js']
    },
    performance: {
        hints: false
    },
    devtool: 'source-map'
}