<template>
	<div class="preview-ui-container" :style="containerCss">
		<div class="preview-ui-content" :style="contentCss">
			<PreviewUIItem v-for="item in items" :obj="item" :global="global" :key="item.id"></PreviewUIItem>
		</div>
	</div>
</template>

<script>
import { mapState } from 'vuex'
import PreviewUIItem from './PreviewUIItem'
export default {
	name: 'PreviewUI',
	components: {
		PreviewUIItem
	},
	computed: {
		...mapState('layout', {
			obj: 'layout'
		}),
		value() {

			return this.obj.value;
		},
		horizontal() {

			return this.value.orientation.value === 'horizontal';
		},
		border() {

			return this.value.border && this.value.border.value;
		},
		items() {

			return this.value.items;
		},
		cssWidth() {

			if (this.horizontal)
				return;

			let value = this.value.size.value;
			return value + 'px';
		},
		cssHeight() {

			if (!this.horizontal)
				return;

			let value = this.value.size.value;
			return value + 'px';
		},
		cssflexDirection() {

			return this.horizontal ? 'row' : 'column';
		},
		cssOpacity() {

			let value = this.value.opacity && this.value.opacity.value;
			return value;
		},
		cssBorderWidth() {

			let rect = this.border && this.border.thickness && this.border.thickness.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		cssBorderColor() {

			let colorDef = this.border && this.border.color && this.border.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		cssBorderRadius() {

			let rect = this.border && this.border.radius && this.border.radius.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 + 5) : el) + 'px').join(' ');
			return value;
		},
		cssBorderImage() {

			let colorDef = this.border && this.border.color && this.border.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'gradient')
				return;

			let stops = colorDef.values;
			let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
				.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
			let out = `linear-gradient(to top, ${values.join(', ')})`;

			return out;
		},
		cssBackgroundColor() {

			let colorDef = this.value.color && this.value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		cssBackgroundImage() {

			let colorDef = this.value.color && this.value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'gradient')
				return;

			let stops = colorDef.values;
			let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
				.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
			let out = `linear-gradient(to top, ${values.join(', ')})`;

			return out;
		},
		cssPadding() {

			let rect = this.value.margin && this.value.margin.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		cssBorderInnerRadius() {

			if (!this.cssBorderRadius)
				return;

			let rect = this.border && this.border.radius && this.border.radius.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 - 5) : el) + 'px').join(' ');
			return value;
		},
		containerCss() {

			let style = {
				opacity: this.cssOpacity,
				padding: this.cssBorderWidth,
				backgroundColor: this.cssBorderColor,
				borderRadius: this.cssBorderRadius,
				backgroundImage: this.cssBorderImage
			};

			return style;
		},
		contentCss() {

			let style = {
				width: this.cssWidth,
				height: this.cssHeight,
				flexDirection: this.cssflexDirection,
				backgroundColor: this.cssBackgroundColor,
				backgroundImage: this.cssBackgroundImage,
				borderRadius: this.cssBorderInnerRadius,
				padding: this.cssPadding
			};

			return style;
		},
		global() {

			let out = {
				horizontal: this.horizontal
			};

			let defaultStyle = this.value['default-button-style'];
			if (defaultStyle) out.defaultStyle = defaultStyle.value;

			return out;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.preview-ui-container {
	margin: auto;
	display: flex;
	align-items: stretch;
}

.preview-ui-content {
	display: flex;
	flex-direction: column;
	align-items: stretch;
}

</style>
