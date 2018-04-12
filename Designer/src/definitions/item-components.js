import PropertyItemContent from '@/components/PropertyItemContent';
import PropertyItemColor from '@/components/PropertyItemColor';
import PropertyItemRectangle from '@/components/PropertyItemRectangle';
import InputAsset from '@/components/InputAsset';
import InputOption from '@/components/InputOption';
import InputNumber from '@/components/InputNumber';
import TextStyleType from '@/types/TextStyleType';
const fontWeightOptions = TextStyleType.prototype.$definition['font-weight'].data.options;

const components = [{
	label: 'Content',
	path: 'style.base.content',
	type: PropertyItemContent
}, {
	label: 'Font Family',
	path: 'style.base.text-style.font-family',
	type: InputAsset,
	options: {
		prefix: './',
		exts: 'ttf'
	}
}, {
	label: 'Font Size',
	path: 'style.base.text-style.font-size',
	type: InputNumber,
	options: {
		min: 1,
		step: 1
	}
}, {
	label: 'Font Weight',
	path: 'style.base.text-style.font-weight',
	type: InputOption,
	options: {
		options: fontWeightOptions
	}
}, {
	label: 'Text Color',
	path: 'style.base.text-style.color',
	type: PropertyItemColor
}, {
	label: 'Background Color',
	path: 'style.base.color',
	type: PropertyItemColor
}, {
	label: 'Margin',
	path: 'style.base.margin',
	type: PropertyItemRectangle
}, {
	label: 'Padding',
	path: 'style.base.padding',
	type: PropertyItemRectangle
}, {
	label: 'Border Thickness',
	path: 'style.base.border.thickness',
	type: PropertyItemRectangle
}, {
	label: 'Border Roundness',
	path: 'style.base.border.radius',
	type: PropertyItemRectangle,
	options: { corner: true }
}, {
	label: 'Border Color',
	path: 'style.base.border.color',
	type: PropertyItemColor
}, {
	label: 'Opacity',
	path: 'style.base.opacity',
	type: InputNumber,
	options: {
		min: 0,
		max: 1,
		step: 0.001
	}
}, {
	label: 'Active Content',
	path: 'style.active.content',
	type: PropertyItemContent
}, {
	label: 'Active Font Size',
	path: 'style.active.text-style.font-size',
	type: InputNumber,
	options: {
		min: 1,
		step: 1
	}
}, {
	label: 'Active Font Family',
	path: 'style.active.text-style.font-family',
	type: InputAsset,
	options: {
		prefix: './',
		exts: 'ttf'
	}
}, {
	label: 'Active Font Weight',
	path: 'style.active.text-style.font-weight',
	type: InputOption,
	options: {
		options: fontWeightOptions
	}
}, {
	label: 'Active Text Color',
	path: 'style.active.text-style.color',
	type: PropertyItemColor
}, {
	label: 'Active Background Color',
	path: 'style.active.color',
	type: PropertyItemColor
}, {
	label: 'Active Margin',
	path: 'style.active.margin',
	type: PropertyItemRectangle
}, {
	label: 'Active Padding',
	path: 'style.active.padding',
	type: PropertyItemRectangle
}, {
	label: 'Active Border Thickness',
	path: 'style.active.border.thickness',
	type: PropertyItemRectangle
}, {
	label: 'Active Border Roundness',
	path: 'style.active.border.radius',
	type: PropertyItemRectangle,
	options: { corner: true }
}, {
	label: 'Active Border Color',
	path: 'style.active.border.color',
	type: PropertyItemColor
}, {
	label: 'Active Opacity',
	path: 'style.active.opacity',
	type: InputNumber,
	options: {
		min: 0,
		max: 1,
		step: 0.001
	}
}];

export default components;
