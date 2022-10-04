module.exports = {
	stats: "verbose",
	performance: {
		hints: false
	},
	output: {
		filename: "[name].js",
		uniqueName: "vuexy",
		publicPath: "auto"
	},	
	resolve: {
		extensions: [".scss"],
		alias: {
            'inferno': 'inferno/dist/index.dev.esm.js'			
		}
	}	
};