<template>
	<div>
		<span>Source</span>
		<select v-model="value">
			<option v-for="filename in assets" :key="filename">{{ filename }}</option>
		</select>
	</div>
</template>

<script>
import StringType from '../types/StringType';
export default {
	name: 'InputLoadedFile',
	props: {
		obj: { type: StringType },
		filter: { type: String }
	},
	computed: {
		assets() {

			let assets = this.$store.getters.assets;
			let { filter } = this;
			if (typeof filter !== 'string')
				filter = '';

			let exts = filter.split(',').filter(i => !!i);
			return assets.filter(i => {
				let parts = i.split('.');
				let ext = parts[parts.length - 1];
				return exts.includes(ext);
			});
		},
		value: {
			get() {

				let obj = this.obj || {};
				return obj.value;
			},
			set(value) {

				this.$store.commit({
					type: 'setValue',
					obj: this.obj,
					value
				});
			}
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
