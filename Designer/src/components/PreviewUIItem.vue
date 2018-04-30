<template>
	<div class="preview-item-container"
		:style="containerCss"
		@click.stop="handleClick"
		@touchstart="onTouchStart"
		@touchend="onTouchEnd"
		@mouseenter="onMouseEnter"
	>
		<div class="preview-item-content" :style="contentCss">
			<span v-if="contentText"><span :style="contentTextCss" v-html="contentText"></span></span>
			<span v-else-if="contentImage">
				<svg v-if="contentImage.svg">
					<filter :id="contentImage.svg.id" color-interpolation-filters="sRGB" x="0" y="0" height="100%" width="100%">
						<feColorMatrix type="matrix" :values="contentImage.svg.matrix" />
					</filter>
				</svg>
				<img :src="contentImage.asset.url" :style="contentImage.css"/>
			</span>
		</div>
	</div>
</template>

<script>
/* eslint no-mixed-operators: 0 */
import { get } from 'lodash'
import { mapGetters, mapActions } from 'vuex'
import ItemType from '@/types/ItemType'
export default {
	name: 'PreviewUIItem',
	props: {
		obj: { type: ItemType },
		global: { type: Object },
		interactive: { type: Boolean },
		forceActive: { type: Boolean }
	},
	data() {
		return {
			isActive: false
		}
	},
	computed: {
		...mapGetters({
			assetItem: 'assets/item'
		}),
		active() { return this.forceActive || this.isActive; },
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

			let activeOut = this.active && (this.getContentText(this.activeStyle) || this.getContentText(this.defaultActiveStyle));
			let out = activeOut || this.getContentText(this.baseStyle) || this.getContentText(this.defaultBaseStyle);

			// Special fallback
			if (out === undefined && this.type === 'move')
				out = '☰☰';

			return out;
		},
		contentImage() {

			let activeOut = this.active && (this.getContentImage(this.activeStyle) || this.getContentImage(this.defaultActiveStyle));
			return activeOut || this.getContentImage(this.baseStyle) || this.getContentImage(this.defaultBaseStyle);
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

			let activeOut = this.active && (this.getCssOpacity(this.activeStyle) || this.getCssOpacity(this.defaultActiveStyle));
			return activeOut || this.getCssOpacity(this.baseStyle) || this.getCssOpacity(this.defaultBaseStyle);
		},
		cssBorderWidth() {

			let activeOut = this.active && (this.getCssBorderWidth(this.activeStyle) || this.getCssBorderWidth(this.defaultActiveStyle));
			return activeOut || this.getCssBorderWidth(this.baseStyle) || this.getCssBorderWidth(this.defaultBaseStyle);
		},
		cssBorderColor() {

			let activeOut = this.active && (this.getCssBorderColor(this.activeStyle) || this.getCssBorderColor(this.defaultActiveStyle));
			return activeOut || this.getCssBorderColor(this.baseStyle) || this.getCssBorderColor(this.defaultBaseStyle);
		},
		cssBorderRadius() {

			let activeOut = this.active && (this.getCssBorderRadius(this.activeStyle) || this.getCssBorderRadius(this.defaultActiveStyle));
			return activeOut || this.getCssBorderRadius(this.baseStyle) || this.getCssBorderRadius(this.defaultBaseStyle);
		},
		cssBorderImage() {

			let activeOut = this.active && (this.getCssBorderImage(this.activeStyle) || this.getCssBorderImage(this.defaultActiveStyle));
			return activeOut || this.getCssBorderImage(this.baseStyle) || this.getCssBorderImage(this.defaultBaseStyle);
		},
		cssBackground() {

			let activeOut = this.active && (this.getCssBackground(this.activeStyle) || this.getCssBackground(this.defaultActiveStyle));
			return activeOut || this.getCssBackground(this.baseStyle) || this.getCssBackground(this.defaultBaseStyle);
		},
		cssMargin() {

			let activeOut = this.active && (this.getCssMargin(this.activeStyle) || this.getCssMargin(this.defaultActiveStyle));
			return activeOut || this.getCssMargin(this.baseStyle) || this.getCssMargin(this.defaultBaseStyle);
		},
		cssPadding() {

			let activeOut = this.active && (this.getCssPadding(this.activeStyle) || this.getCssPadding(this.defaultActiveStyle));
			return activeOut || this.getCssPadding(this.baseStyle) || this.getCssPadding(this.defaultBaseStyle);
		},
		cssBorderInnerRadius() {

			if (!this.cssBorderRadius)
				return;

			let activeOut = this.active && (this.getCssBorderInnerRadius(this.activeStyle) || this.getCssBorderInnerRadius(this.defaultActiveStyle));
			return activeOut || this.getCssBorderInnerRadius(this.baseStyle) || this.getCssBorderInnerRadius(this.defaultBaseStyle);
		},
		cssFontSize() {

			let activeOut = this.active && (this.getCssFontSize(this.activeStyle) || this.getCssFontSize(this.defaultActiveStyle));
			return activeOut || this.getCssFontSize(this.baseStyle) || this.getCssFontSize(this.defaultBaseStyle);
		},
		cssFontWeight() {

			let activeOut = this.active && (this.getCssFontWeight(this.activeStyle) || this.getCssFontWeight(this.defaultActiveStyle));
			return activeOut || this.getCssFontWeight(this.baseStyle) || this.getCssFontWeight(this.defaultBaseStyle);
		},
		cssFontFamily() {

			let activeOut = this.active && (this.getCssFontFamily(this.activeStyle) || this.getCssFontFamily(this.defaultActiveStyle));
			return activeOut || this.getCssFontFamily(this.baseStyle) || this.getCssFontFamily(this.defaultBaseStyle);
		},
		cssColor() {

			let activeOut = this.active && (this.getCssColor(this.activeStyle) || this.getCssColor(this.defaultActiveStyle));
			return activeOut || this.getCssColor(this.baseStyle) || this.getCssColor(this.defaultBaseStyle);
		},
		cssTextImageColor() {

			let activeOut = this.active && (this.getCssTextImageColor(this.activeStyle) || this.getCssTextImageColor(this.defaultActiveStyle));
			return activeOut || this.getCssTextImageColor(this.baseStyle) || this.getCssTextImageColor(this.defaultBaseStyle);
		},
		containerCss() {

			let style = {
				width: this.cssWidth,
				height: this.cssHeight,
				opacity: this.cssOpacity,
				backgroundColor: this.cssBorderColor,
				borderRadius: this.cssBorderRadius,
				backgroundImage: this.cssBorderImage,
				margin: this.cssMargin,
				cursor: this.cursorCss
			};

			return style;
		},
		contentCss() {

			let style = {
				flexDirection: this.cssflexDirection,
				background: this.cssBackground,
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
		contentTextCss() {

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
		},
		cursorCss() {

			return this.type === 'move' ? 'move' : 'auto';
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
		onMouseEnter(ev) {

			this.$emit('mouseenter', { obj: this.obj });
		},
		onTouchStart() {

			/* eslint no-fallthrough: 0 */
			if (!this.interactive) return;

			switch (this.type) {
			case 'tap':
			case 'radial-menu':
				setTimeout(() => { this.isActive = false; }, 0);
			case 'hold':
			case 'move':
				this.isActive = true;
				break;
			case 'toggle':
				this.isActive = !this.isActive;
				break;
			}
		},
		onTouchEnd() {

			if (!this.interactive) return;

			switch (this.type) {
			case 'hold':
			case 'move':
				this.isActive = false;
				break;
			}
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

			let css = {};
			let marginRect = get(content, 'margin.value');
			if (marginRect) {
				let value = marginRect.split(' ').map(el => el + 'px').join(' ');
				css.margin = value;
			}

			let opacity = get(content, 'opacity.value');
			if (opacity !== undefined)
				css.opacity = opacity;

			let svg;
			let colorizeColor = get(content, 'colorize.value');
			if (colorizeColor) {
				svg = {
					id: 'content-colorize-' + content.colorize.id,
					matrix: colorizeColor.getColorizeMatrix()
				}
				css.filter = `url(#${svg.id})`;
			}

			return {
				asset,
				css,
				svg
			};
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

			let value = rect.split(' ').map(el => el + 'px').join(' ');
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
		getCssBackground(style) {

			let colorDef = style && style.color && style.color.value;
			if (!colorDef)
				return;

			let out;
			switch (colorDef.type.value) {
			case 'solid':
				let color = colorDef.value.value;
				out = color.getHex();
				break;

			case 'gradient':
				let stops = colorDef.values.slice(0);
				let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
					.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
				out = `linear-gradient(to top, ${values.join(', ')})`;
				break;
			}

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

			let value = rect.split(' ').map(el => el + 'px').join(' ');
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

			if (name === undefined)
				return;

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
	},
	watch: {
		isActive(value) {

			let command = this.obj.getObj('command');
			if (!command)
				return;

			command = command.value;
			let eventName = value ? 'activecommand' : 'inactivecommand';
			this.$emit(eventName, { command });
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* Default style */
.preview-item-container {
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
	flex: 1 1 auto;
	object-fit: contain;
}

</style>
