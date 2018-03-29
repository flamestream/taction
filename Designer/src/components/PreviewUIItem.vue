<template>
	<div class="preview-item-container" :style="containerCss" @click.stop="handleClick">
		<div class="preview-item-content" :style="contentCss">
			<span v-if="contentText"><span :style="textCss" v-html="contentText"></span></span>
			<img v-else-if="contentImage" :src="contentImage.url"/>
		</div>
	</div>
</template>

<script>
/* eslint no-mixed-operators: 0 */
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
		value() { return this.obj && this.obj.value; },
		type() { return this.value.type.value; },
		size() { return this.value.size.value; },
		style() { return this.value.style && this.value.style.value || {}; },
		baseStyle() { return this.style.base && this.style.base.value || {}; },
		activeStyle() { return this.style.active && this.style.active.value || {}; },
		defaultStyle() { return this.global && this.global.defaultStyle || {}; },
		defaultBaseStyle() { return this.defaultStyle.base && this.defaultStyle.base.value || {}; },
		defaultActiveStyle() { return this.defaultStyle.active && this.defaultStyle.active.value || {}; },
		baseContent() { return this.baseStyle.content && this.baseStyle.content.value; },
		contentText() {

			let out = this.getContentText(this.baseStyle) || this.getContentText(this.defaultBaseStyle);

			// Special fallback
			if (out === undefined && this.type === 'move')
				out = '☰☰';

			return out;
		},
		contentImage() {

			return this.getContentImage(this.baseStyle) || this.getContentImage(this.defaultBaseStyle);
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

			return this.getCssOpacity(this.baseStyle) || this.getCssOpacity(this.defaultBaseStyle);
		},
		cssBorderWidth() {

			return this.getCssBorderWidth(this.baseStyle) || this.getCssBorderWidth(this.defaultBaseStyle);
		},
		cssBorderColor() {

			return this.getCssBorderColor(this.baseStyle) || this.getCssBorderColor(this.defaultBaseStyle);
		},
		cssBorderRadius() {

			return this.getCssBorderRadius(this.baseStyle) || this.getCssBorderRadius(this.defaultBaseStyle);
		},
		cssBorderImage() {

			return this.getCssBorderImage(this.baseStyle) || this.getCssBorderImage(this.defaultBaseStyle);
		},
		cssBackgroundColor() {

			return this.getCssBackgroundColor(this.baseStyle) || this.getCssBackgroundColor(this.defaultBaseStyle);
		},
		cssBackgroundImage() {

			return this.getCssBackgroundImage(this.baseStyle) || this.getCssBackgroundImage(this.defaultBaseStyle);
		},
		cssMargin() {

			return this.getCssMargin(this.baseStyle) || this.getCssMargin(this.defaultBaseStyle);
		},
		cssPadding() {

			return this.getCssPadding(this.baseStyle) || this.getCssMargin(this.defaultBaseStyle);
		},
		cssBorderInnerRadius() {

			if (!this.cssBorderRadius)
				return;

			return this.getCssBorderInnerRadius(this.baseStyle) || this.getCssBorderInnerRadius(this.defaultBaseStyle);
		},
		cssFontSize() {

			return this.getCssFontSize(this.baseStyle) || this.getCssFontSize(this.defaultBaseStyle);
		},
		cssFontWeight() {

			return this.getCssFontWeight(this.baseStyle) || this.getCssFontWeight(this.defaultBaseStyle);
		},
		cssFontFamily() {

			return this.getCssFontFamily(this.baseStyle) || this.getCssFontFamily(this.defaultBaseStyle);
		},
		cssColor() {

			return this.getCssColor(this.baseStyle) || this.getCssColor(this.defaultBaseStyle);
		},
		cssTextImageColor() {

			return this.getCssTextImageColor(this.baseStyle) || this.getCssTextImageColor(this.defaultBaseStyle);
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
		},
		getContentText(style) {

			let content = style.content && style.content.value;
			if (!content)
				return;

			let type = content.type.value;
			if (type !== 'text')
				return;

			let value = content.value.value;
			if (!value)
				return;

			value = value.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/\s/g, '&nbsp;');
			return value;
		},
		getContentImage(style) {

			let content = style.content && style.content.value;
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
		getCssOpacity(style) {

			let value = style.opacity && style.opacity.value;

			if (value === undefined)
				return;

			return value;
		},
		getCssBorderWidth(style) {

			let rect = style && style.border && style.border.value && style.border.value.thickness && style.border.value.thickness.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		getCssBorderColor(style) {

			let colorDef = style && style.border && style.border.value && style.border.value.color && style.border.value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		getCssBorderRadius(style) {

			let rect = style && style.border && style.border.value && style.border.value.radius && style.border.value.radius.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 + 5) : el) + 'px').join(' ');
			return value;
		},
		getCssBorderImage(style) {

			let colorDef = style && style.border && style.border.value && style.border.value.color && style.border.value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'gradient')
				return;

			let stops = colorDef.values.slice(0);
			let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
				.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
			let out = `linear-gradient(to top, ${values.join(', ')})`;

			return out;
		},
		getCssBackgroundColor(style) {

			let colorDef = style && style.color && style.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		getCssBackgroundImage(style) {

			let colorDef = style && style.color && style.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'gradient')
				return;

			let stops = colorDef.values.slice(0);
			let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
				.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
			let out = `linear-gradient(to top, ${values.join(', ')})`;

			return out;
		},
		getCssMargin(style) {

			let rect = style && style.margin && style.margin.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		getCssPadding(style) {

			let rect = style && style.padding && style.padding.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => el + 'px').join(' ');
			return value;
		},
		getCssBorderInnerRadius(style) {

			let rect = style && style.border && style.border.value && style.border.value.radius && style.border.value.radius.value;
			if (!rect)
				return;

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 + 4) : el) + 'px').join(' ');
			return value;
		},
		getCssFontSize(style) {

			let value = style && style['text-style'] && style['text-style'].value && style['text-style'].value['font-size'] && style['text-style'].value['font-size'].value;

			if (value === undefined)
				return;

			return value + 'px';
		},
		getCssFontWeight(style) {

			let name = style && style['text-style'] && style['text-style'].value && style['text-style'].value['font-weight'] && style['text-style'].value['font-weight'].value;

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
		getCssFontFamily(style) {

			let value = style && style['text-style'] && style['text-style'].value && style['text-style'].value['font-family'] && style['text-style'].value['font-family'].value;

			if (value === undefined)
				return;

			return `"${value}"`;
		},
		getCssColor(style) {

			let colorDef = style && style['text-style'] && style['text-style'].value && style['text-style'].value.color && style['text-style'].value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'solid')
				return;

			let color = colorDef.value.value;

			return color.getHex();
		},
		getCssTextImageColor(style) {

			let colorDef = style && style['text-style'] && style['text-style'].value && style['text-style'].value.color && style['text-style'].value.color.value;
			if (!colorDef)
				return;

			if (colorDef.type.value !== 'gradient')
				return;

			let stops = colorDef.values.slice(0);
			let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
				.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
			let out = `linear-gradient(to top, ${values.join(', ')})`;

			return out;
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
	font-family: 'Segoe UI', sans-serif;
	font-size: 12px;
}

.preview-item-content > span {
	display: flex;
	flex-direction: column;
	justify-content: center;
	overflow: hidden;
	flex: 1 1 auto;
}

.preview-item-content > span > span {
	margin: auto;
}

.preview-item-content img {
	width: 100%;
	height: 100%;
	object-fit: contain;
}

</style>
