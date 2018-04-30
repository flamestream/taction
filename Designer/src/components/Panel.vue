<template>
	<div class="panel" :style="cssContainer" oncontextmenu="return false">
		<div class="panel-content"
				:style="css"
				@touchstart="onTouchStart"
				@touchmove="onTouchMove"
				@mousemove="onMouseMove"
				@mouseleave="onMouseLeave">
			<PreviewUIItem v-for="item in items" :key="item.id"
				ref="items"
				:obj="item"
				:global="global"
				:interactive="interactive"
				:forceActive="forceActive"
				@activecommand="onActiveCommand"
				@inactivecommand="onInactiveCommand"
				@mouseenter="onItemMouseEnter">
			</PreviewUIItem>
		</div>
	</div>
</template>

<script>
import LayoutType from '@/types/LayoutType'
import PreviewUIItem from './PreviewUIItem'
export default {
	name: 'Panel',
	components: {
		PreviewUIItem
	},
	props: {
		obj: { type: LayoutType },
		interactive: { type: Boolean },
		forceActive: { type: Boolean }
	},
	data() {
		return {
			isTouchTriggered: false,
			isMouseOver: false,
			isMouseOverMover: false
		}
	},
	computed: {
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

			let value = this.interactive && this.isMouseOver && !this.isMouseOverMover
				? this.obj.getObj('opacity-hide', true, true)
				: this.obj.getObj('opacity', true, true);

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

			let borderWidth = this.obj.getObj('border.thickness', false, true);
			if (!borderWidth)
				return;

			let stops = colorDef.values.slice(0);
			let values = stops.sort((a, b) => a.value.position.value - b.value.position.value)
				.map(el => `${el.value.color.value.getHex()} ${el.value.position.value * 100}%`);
			let out = `linear-gradient(to top, ${values.join(', ')}) ${borderWidth}`;

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
		cssTransition() {

			let fadeTime = this.obj.getObj('fade-animation-time', true, true);
			let value = `all 0.5s ease-out, opacity ${fadeTime * 0.001}s ease-out`;
			return value;
		},
		cssContainer() {
			let style = {
				opacity: this.cssOpacity,
				overflow: 'hidden',
				borderWidth: '0px',
				borderStyle: 'solid',
				borderRadius: this.cssBorderRadius,
				transition: this.cssTransition
			};

			return style;
		},
		css() {

			let style = {
				width: this.cssWidth,
				height: this.cssHeight,
				flexDirection: this.cssflexDirection,
				borderColor: this.cssBorderColor,
				borderImage: this.cssBorderImage,
				borderWidth: this.cssBorderWidth || 0,
				borderStyle: 'solid',
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
		onTouchStart() {
			this.isTouchTriggered = true;
		},
		onTouchMove() {
			this.isTouchTriggered = true;
		},
		onMouseMove(ev) {

			if (this.isTouchTriggered) {

				this.isTouchTriggered = false;
				return;
			}

			this.isMouseOver = true;
		},
		onMouseLeave() {
			this.isMouseOver = false;
		},
		onItemMouseEnter(ev) {

			let {obj} = ev;
			let type = obj.getObj('type', true, true);
			this.isMouseOverMover = type === 'move';
		},
		onActiveCommand(ev) {

			if (!this.interactive)
				return;

			this.$emit('activecommand', ev);
		},
		onInactiveCommand(ev) {

			if (!this.interactive)
				return;

			this.$emit('inactivecommand', ev);
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.panel {
	display: inline-block;
}

.panel-content {
	margin: 16px;
	display: flex;
}

</style>
