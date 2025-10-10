module.exports = {
    experiments: {
        topLevelAwait: true
    },
    resolve: {
        fallback: { 
            url: require.resolve('url'),
            fs: false,
            dgram: false,
            assert: require.resolve('assert'),
            crypto: require.resolve('crypto-browserify'),
            http: require.resolve('stream-http'),
            https: require.resolve('https-browserify'),
            os: require.resolve('os-browserify'),
            buffer: require.resolve('buffer'),
            stream: require.resolve('stream-browserify'),
            timers: require.resolve('timers-browserify'),
            path: require.resolve('path-browserify'),
            zlib: require.resolve('browserify-zlib')
        }
    },
    optimization: {
        splitChunks: {
            chunks: 'all',
            maxInitialRequests: 20,
            maxAsyncRequests: 20,
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/]/,
                    name: 'vendors',
                    chunks: 'all',
                    priority: 10
                },
                common: {
                    name: 'common',
                    chunks: 'all',
                    priority: 5,
                    minChunks: 2,
                    reuseExistingChunk: true
                }
            }
        }
    },
    performance: {
        maxEntrypointSize: 15000000,  // 8MB
        maxAssetSize: 15000000,      // 8MB
        hints: false
    }
}
