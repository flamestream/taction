<template>
	<div class="preview-ui-container" :style="containerCss">
		<div class="preview-ui-content-container" ref="contentContainer">
			<label>Base</label>
			<div class="preview-ui-content" :style="contentCss">
				<PreviewUIItem v-for="item in items" ref="items" :obj="item" :global="global" :key="item.id"></PreviewUIItem>
			</div>
			<transition name="highlight" @after-enter="onHighlightAfterEnter">
				<div v-show="highlightActive" class="highlight"></div>
			</transition>
		</div>
		<div class="preview-ui-content-container" ref="activeContentContainer">
			<label>Active</label>
			<div class="preview-ui-content" :style="contentCss">
				<PreviewUIItem v-for="item in items" ref="activeItems" :obj="item" :global="global" :active="true" :key="item.id"></PreviewUIItem>
			</div>
			<transition name="highlight">
				<div v-show="highlightActive" class="highlight"></div>
			</transition>
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
	data() {
		return {
			highlightActive: false
		}
	},
	computed: {
		...mapState('layout', {
			obj: 'layout'
		}),
		...mapState('ui', ['activeItem']),
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

			let stops = colorDef.values.slice(0);
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

			let stops = colorDef.values.slice(0);
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

			let value = rect.split(' ').map(el => (el * 1 ? (el * 1 + 4) : el) + 'px').join(' ');
			return value;
		},
		containerCss() {

			let style = {
				opacity: this.cssOpacity,
				padding: this.cssBorderWidth,
				backgroundColor: this.cssBorderColor,
				borderRadius: this.cssBorderRadius,
				backgroundImage: this.cssBorderImage,
				flexDirection: this.horizontal ? 'column' : 'row'
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
	},
	methods: {
		onHighlightAfterEnter() {
			this.highlightActive = false;
		},
		initHighlight(activeItem, itemElmGroup, containerElm) {

			let target = itemElmGroup.find(i => { return i.obj === activeItem; });
			if (!target)
				return;

			let targetRect = target.$el.getBoundingClientRect();
			let containerRect = containerElm.getBoundingClientRect();
			let top = targetRect.top - containerRect.top - 2;
			let left = targetRect.left - containerRect.left - 2;

			let highlight = containerElm.getElementsByClassName('highlight')[0];
			highlight.style.top = top + 'px';
			highlight.style.left = left + 'px';
			highlight.style.width = targetRect.width + 'px';
			highlight.style.height = targetRect.height + 'px';
		}
	},
	watch: {
		activeItem(value) {

			console.log('activeitemchange', value, this.$refs.items);
			if (!value)
				return;

			this.initHighlight(value, this.$refs.items, this.$refs.contentContainer);
			this.initHighlight(value, this.$refs.activeItems, this.$refs.activeContentContainer);
			this.highlightActive = true;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.preview-ui-container {
	flex: 1 1 auto;
	display: flex;
	justify-content: space-equal;
}

.preview-ui-content-container {
	margin: auto;
	position: relative;
}

.preview-ui-content-container > label {
	position: absolute;
	border: 1px solid #1B283855;
	display: inline-block;
	padding: 2px 6px;
	font-size: 10px;
	background-color: #1B2838;
	color: #FFFFFF;
	border-radius: 3px;
	left: 8px;
}

.preview-ui-content {
	display: flex;
	margin: 24px 8px 8px;
}

.highlight {
	position: absolute;
	top: 0;
	left: 0;
	border: 2px solid #FF0000;
	opacity: 0.6;
}

.highlight-enter-active {
	transition: 0;
}
.highlight-leave-active {
 	transition: all 0.5s ease-out;
}
.highlight-leave-to {
	transform: scale(1.5);
	opacity: 0;
	border-width: 0;
}

</style>
