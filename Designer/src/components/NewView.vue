<template>
	<div class="reset-view" @click.stop>
		<div class="selection">
			<div v-for="layout in layouts" :key="layout.name" class="item" @click.stop="onItemClick(layout)">
				<div class="preview" :src="layout.previewImageUrl">
					<i class="material-icons">remove_circle</i>
					<span>No Preview</span>
				</div>
				<label>{{ layout.name }}</label>
			</div>
		</div>
	</div>
</template>

<script>
import { mapActions } from 'vuex'
import layouts from '@/definitions/layouts.json'
import importAsync from '@/helpers/import-async'
export default {
	name: 'NewView',
	data() {
		return {
			layouts
		}
	},
	methods: {
		...mapActions({
			reset: 'reset',
			setErrorMsg: 'ui/setErrorMsg',
			setActiveOverlay: 'ui/setActiveOverlay'
		}),
		async onItemClick(item) {

			if (item.code) {

				this.reset({ layout: item.code });
				this.setActiveOverlay();

			} else if (item.url) {

				try {

					// @TODO Progress/wait
					let resp = await fetch(item.url);
					if (resp.status !== 200)
						throw new Error('Could not fetch layout file');

					let blob = await resp.blob();

					let file;
					try {

						file = new File([blob], resp.url);

					} catch (e) { // Assume msedge issue

						file = blob;
						file.lastModifiedDate = new Date();
						file.name = resp.url;
						file.__proto__ = File.prototype;
					}

					let imported = await importAsync({file});
					let layout = imported.layout;
					let zip = imported.zip;

					this.reset({layout, zip});
					this.setActiveOverlay();

				} catch (err) {

					this.setErrorMsg({msg: 'Error loading layout: ' + err.message});
					this.setActiveOverlay({id: 'error'});
				}
			}

		}
	},
	mounted() {
		this.activeLayout = this.layouts[0];
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.reset-view {
	color: #0D141A;
	background-color: #FFF;
/*	width: 650px;
	height: 200px;*/
	border-radius: 3px;
	transition: all 0.5s;
	display: flex;
	flex-direction: column;
	padding: 12px;
	font-size: 14px;
}

.selection {
	flex: 1 1 auto;
	display: flex;
	justify-content: space-evenly;
}

.actions {
	display: flex;
	justify-content: flex-end;
}

.item {
	width: 150px;
	height: 170px;
	padding: 8px;
	font-size: 14px;
	margin-right: 8px;
	overflow: hidden;
	display: flex;
	flex-direction: column;
	border-radius: 4px;
	transition: all 0.2s ease-out;
}
.item:last-child {
	margin-right: 0;
}
.item:hover {
	background-color: #38BEEA66;

}

.preview {
	flex: 1 1 auto;
	display: flex;
	position: relative;
	flex-direction: column;
	color: #DDD;
}
.item:hover .preview {
	color: #FFF;
}

.preview span {
	white-space: nowrap;
	margin: auto;
	margin-top: 0;
	font-weight: bold;
}

label {
	overflow: hidden;
	text-overflow: ellipsis;
	white-space: nowrap;
	margin-top: 4px;
}

i {
	margin: auto;
	font-size: 80px;
}

</style>
