// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import store from './store/index'

Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
	el: '#app',
	store,
	components: { App },
	template: '<App/>',
	mounted () {

		// Font registering
		this.$store.watch(() => this.$store.getters['assets/fonts'], fonts => {

			let style = document.getElementById('font-style');

			let innerHTML = '';
			for (let name in fonts) {

				let font = fonts[name];
				innerHTML += `@font-face { font-family: './${name}'; src: url('${font.url}'); }\n`
			}

			style.innerHTML = innerHTML;

		}, { deep: true });

		// Font registering
		this.$store.watch(() => this.$store.getters['ui/activeFont'], font => {

			let innerHTML = '';
			if (font)
				innerHTML = `@font-face { font-family: './Active Font'; src: url('${font.url}'); }`;

			let style = document.getElementById('active-font-style');
			style.innerHTML = innerHTML;

		}, { deep: true });
	}
});

document.addEventListener('dragover', function(ev) {
	ev.preventDefault();
}, true);
