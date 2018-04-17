<template>
	<div class="root">
		<label v-if="label" class="label">{{ label }}</label>
		<select v-model="value">
			<option v-for="filename in filteredAssets" :key="filename">{{ filename }}</option>
		</select>
		<label class="button" @click="onAddAssetClick"><i class="material-icons">add</i></label>
	</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import { get } from 'lodash'
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
		label() {
			return get(this, 'options.label');
		},
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

				// let options = this.options || {};
				// let {prefix} = options;
				// if (prefix)
				// 	value = prefix + value;

				this.$store.commit({
					type: 'layout/setValue',
					obj: this.obj,
					value
				});
			}
		}
	},
	methods: {
		...mapActions({
			setActiveMenu: 'ui/setActiveMenu'
		}),
		onAddAssetClick(ev) {
			this.setActiveMenu({id: 'assets'});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.root {
	display: flex;
	flex-direction: row;
}

.label {
	margin-right: 4px;
}

select {
	flex: 1 1 auto;
	border-top-right-radius: 0;
	border-bottom-right-radius: 0;
}

.button {
	font-size: 0;
	border-top-left-radius: 0;
	border-bottom-left-radius: 0;
}

.button:active {
	top: 0;
	left: 0;
}

i {
	font-size: 16px;
}

.button:active i {
	position: relative;
	top: 1px;
	left: 1px;
}

</style>
