<template>
	<div>
		<div :class="getInputClassNames()">
			<input ref="input" type="file" label="Add asset" id="file-asset" @change="handleFileChange" multiple/>
			<label for="file-asset" @click="handleFileClick">{{ fileButtonLabel }}</label>
		</div>
		<div class="asset-list">
			<div class="menu-item" v-for="name in assetNames" :class="getAssetClassNames(name)" @click="handleAssetClick" :data-id="name" :key="name">
				<span class="label">{{ name }}</span>
				<span v-if="isActive(name)" class="command" @click.stop="handleRemoverClick">－</span>
			</div>
		</div>
	</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import PropertyItem from './PropertyItem'
import config from '../config'
export default {
	name: 'ViewAssets',
	components: {
		PropertyItem
	},
	data() {
		return {
			defaultFileButtonLabel: 'Add a new asset',
			fileButtonError: undefined
		}
	},
	computed: {
		...mapGetters(['assetNames', 'activeAssetName']),
		fileButtonLabel() {

			return this.fileButtonError || this.defaultFileButtonLabel;
		}
	},
	methods: {
		...mapActions(['setActiveAsset', 'addAsset', 'removeAsset']),
		isActive(assetName) {

			return assetName === this.activeAssetName;
		},
		getInputClassNames() {

			return {
				input: true,
				error: !!this.fileButtonError
			}
		},
		getAssetClassNames(assetName) {

			return {
				active: this.isActive(assetName)
			}
		},
		handleAssetClick(ev) {

			let { currentTarget } = ev;
			let { id } = currentTarget.dataset;
			this.setActiveAsset({id});
		},
		handleFileClick(ev) {

			this.$refs.input.value = null;
			this.fileButtonError = undefined;
		},
		async handleFileChange(ev) {

			let {files} = ev.target;

			for (let file of files) {

				let objectURL = URL.createObjectURL(file);
				console.log(objectURL);
				console.log(file);

				try {

					await this.addAsset({
						file,
						active: true
					});

				} catch (err) {

					console.error(err.stack);
					this.fileButtonError = err.message;
					return;
				}
			}
		},
		handleRemoverClick(ev) {

			let { currentTarget } = ev;
			let target = currentTarget.closest('.menu-item');
			let { id } = target.dataset;
			this.removeAsset({id});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.input input[type=file] {
	display: none;
}

.input label {
	background-color: #09B0EB;
	text-align: center;
	font-weight: 600;
	display: block;
	cursor: pointer;
	padding: 4px;
	margin: 0 10px 10px;
	border-radius: 3px;
}

.input.error label {
	background-color: #c10707;
}

.menu-item {
	line-height: 40px;
	border-left: 3px solid transparent;
	display: flex;
}

.menu-item > .label {
	flex: 1 1 auto;
	margin-left: 10px;
}

.menu-item > .command {
	margin-right: 10px;
}

.menu-item.active {
	background-color: #fff;
	color: #273D4E;
	border-left-color: #09B0EB;
	box-shadow: 4px 0px 10px 0px rgba(0, 0, 0, 1);
}

</style>
