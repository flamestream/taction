<template>
	<div class="preview-item-container" :style="containerCss" @click.stop="handleClick">
		<div class="preview-item-content" :style="contentCss">
			<span v-if="contentText"><span :style="textCss">{{ contentText }}</span></span>
			<img v-else-if="contentImage" :src="contentImage.url"/>
		</div>
	</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import ItemType from '@/types/ItemType'
export default {
	name: 'PreviewUIItem',
	props: {
		obj: { type: ItemType },
		global: { type: Object }
	},
	computed: {
		...mapGetters({
			assetItem: 'assets/item'
		}),
		value() {

			let obj = this.obj || {};
			return obj.value;
		},
		type() {

			return this.value.type.value;
		},
		size() {

			return this.value.size.value;
		},
		style() {

			/* eslint no-mixed-operators: 0 */
			return this.value.style && this.value.style.value || {};
		},
		baseStyle() {

			/* eslint no-mixed-operators: 0 */
			return this.style.base && this.style.base.value || {};
		},
		activeStyle() {

			/* eslint no-mixed-operators: 0 */
			return this.style.active && this.style.active.value || {};
		},
		baseContent() {

			return this.baseStyle.content && this.baseStyle.content.value;
		},
		contentText() {

			let text;
			let content = this.baseContent;
			if (content) {

				let type = content.type.value;
				if (type === 'text') {

					text = content.value.value;
				}
			}

			// Special fallback
			if (text === undefined && this.type === 'move')
				text = '☰☰';

			return text;
		},
		contentImage() {

			let content = this.baseContent;
			if (!content)
				return;

			let type = content.type.value;
			if (type !== 'image')
				return;

			let id = content.source && content.source.value;
			if (!id)
				return

			let asset = this.assetItem({id});
			if (!asset)
				return;

			return asset;
		},
		cssWidth() {

			if (!this.global.horizontal)
				return;

			let value = this.value.size.value;
			return value + 'px';
		},
		cssHeight() {

			if (this.global.horizontal)
				return;

			let value = this.value.size.value;
			return value + 'px';
		},
		cssOpacity() {

			let value = this.baseStyle.opacity && this.baseStyle.opacity.value;

			if (value === undefined)
				return;

			return value;
		},
		cssBorderWidth() {

			let rect = this.baseStyle && this.baseStyle.border && this.baseStyle.border.value && this.baseStyle.border.value.thickness && this.baseStyle.border.value.thickness.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		cssBorderColor() {

			let colorDef = this.baseStyle && this.baseStyle.border && this.baseStyle.border.value && this.baseStyle.border.value.color && this.baseStyle.border.value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		cssBorderRadius() {

			let rect = this.baseStyle && this.baseStyle.border && this.baseStyle.border.value && this.baseStyle.border.value.radius && this.baseStyle.border.value.radius.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 + 5) : el) + 'px').join(' ');
			return value;
		},
		cssBorderImage() {

			let colorDef = this.baseStyle && this.baseStyle.border && this.baseStyle.border.value && this.baseStyle.border.value.color && this.baseStyle.border.value.color.value;
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

			let colorDef = this.baseStyle && this.baseStyle.color && this.baseStyle.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		cssBackgroundImage() {

			let colorDef = this.baseStyle && this.baseStyle.color && this.baseStyle.color.value;
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
		cssMargin() {

			let rect = this.baseStyle && this.baseStyle.margin && this.baseStyle.margin.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		cssPadding() {

			let rect = this.baseStyle && this.baseStyle.padding && this.baseStyle.padding.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		cssBorderInnerRadius() {

			if (!this.cssBorderRadius)
				return;

			let rect = this.baseStyle && this.baseStyle.border && this.baseStyle.border.value && this.baseStyle.border.value.radius && this.baseStyle.border.value.radius.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 - 5) : el) + 'px').join(' ');
			return value;
		},
		cssFontSize() {

			let value = this.baseStyle && this.baseStyle['text-style'] && this.baseStyle['text-style'].value && this.baseStyle['text-style'].value['font-size'] && this.baseStyle['text-style'].value['font-size'].value;

			if (value === undefined)
				return;

			return value + 'px';
		},
		cssFontWeight() {

			let name = this.baseStyle && this.baseStyle['text-style'] && this.baseStyle['text-style'].value && this.baseStyle['text-style'].value['font-weight'] && this.baseStyle['text-style'].value['font-weight'].value;

			// @TODO Move this
			let fontWeightValue = {
				'thin': 100,
				'light': 300,
				'normal': 400,
				'bold': 700,
				'heavy': 900
			};

			let value = fontWeightValue[name] || 400;

			return value;
		},
		cssFontFamily() {

			let value = this.baseStyle && this.baseStyle['text-style'] && this.baseStyle['text-style'].value && this.baseStyle['text-style'].value['font-family'] && this.baseStyle['text-style'].value['font-family'].value;

			if (value === undefined)
				return;

			return `"${value}"`;
		},
		cssColor() {

			let colorDef = this.baseStyle && this.baseStyle['text-style'] && this.baseStyle['text-style'].value && this.baseStyle['text-style'].value.color && this.baseStyle['text-style'].value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		cssTextImageColor() {

			let colorDef = this.baseStyle && this.baseStyle['text-style'] && this.baseStyle['text-style'].value && this.baseStyle['text-style'].value.color && this.baseStyle['text-style'].value.color.value;
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
		containerCss() {

			let style = {
				width: this.cssWidth,
				height: this.cssHeight,
				opacity: this.cssOpacity,
				backgroundColor: this.cssBorderColor,
				borderRadius: this.cssBorderRadius,
				backgroundImage: this.cssBorderImage,
				margin: this.cssMargin
			};

			return style;
		},
		contentCss() {

			let style = {
				flexDirection: this.cssflexDirection,
				backgroundColor: this.cssBackgroundColor,
				backgroundImage: this.cssBackgroundImage,
				borderRadius: this.cssBorderInnerRadius,
				margin: this.cssBorderWidth,
				padding: this.cssPadding,
				fontSize: this.cssFontSize,
				fontWeight: this.cssFontWeight,
				color: this.cssColor,
				fontFamily: this.cssFontFamily
			};

			return style;
		},
		textCss() {

			let style = {
				backgroundImage: this.cssTextImageColor
			};

			if (style.backgroundImage) {

				style['-webkit-background-clip'] = 'text';
				style['-webkit-text-fill-color'] = 'transparent';
				style['background-clip'] = 'text';
				style['text-fill-color'] = 'transparent';
			}

			return style;
		}
	},
	methods: {
		...mapActions({
			setActiveItem: 'ui/setActiveItem'
		}),
		getBorder(style) {

			if (!style)
				return;

			/* eslint no-mixed-operators: 0 */
			return this.style.border && this.style.border.value;
		},
		handleClick(ev) {

			let {id} = this.obj;
			this.setActiveItem({id});
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* Default style */
.preview-item-container {
	flex: 1 1 auto;
	display: flex;
	align-items: stretch;
	background-color: #707070;
}

.preview-item-content {
	flex: 1 1 auto;
	display: flex;
	align-items: stretch;
	justify-content: center;
	overflow: hidden;
	white-space: nowrap;
	border-width: 0;
	margin: 1px;

	color: #000000;
	background-color: #DDDDDD;
	font-family: 'Segoe UI';
	font-size: 12px;
}

.preview-item-content > span {
	display: flex;
	flex-direction: column;
	justify-content: center;
	overflow: hidden;
}

.preview-item-content img {
	width: 100%;
	height: 100%;
	object-fit: contain;
}

</style>
