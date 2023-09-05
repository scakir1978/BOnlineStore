module.exports = {
    resolve: {
        fallback: { 
            url: require.resolve('url'),
            fs: require.resolve('fs'),
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
    }    
}