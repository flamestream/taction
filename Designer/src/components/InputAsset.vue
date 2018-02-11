<template>
	<div>
		<span>Source: </span>
		<select v-model="value">
			<option v-for="filename in filteredAssets" :key="filename">{{ filename }}</option>
		</select>
	</div>
</template>

<script>
import { mapGetters } from 'vuex'
import StringType from '@/types/StringType';
export default {
	name: 'InputAsset',
	props: {
		obj: { type: StringType },
		options: { type: Object }
	},
	computed: {
		...mapGetters({
			assetNames: 'assets/names'
		}),
		exts() {

			let options = this.options || {};
			let {exts} = options;

			if (exts && !Array.isArray(exts))
				exts = [exts];

			return exts;
		},
		filteredAssets() {

			let {assetNames} = this;

			// Prefix check
			let options = this.options || {};
			let {prefix} = options;
			if (prefix) assetNames = assetNames.map(n => prefix + n);

			// Filter check
			let {exts} = this;
			if (!exts)
				return assetNames;

			// Filter away
			return assetNames.filter(n => {

				let ext = n.split('.').pop();
				return exts.includes(ext);
			});
		},
		value: {
			get() {

				let obj = this.obj || {};
				return obj.value;
			},
			set(value) {

				let options = this.options || {};
				let {prefix} = options;
				if (prefix)
					value = prefix + value;

				this.$store.commit({
					type: 'layout/setValue',
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
