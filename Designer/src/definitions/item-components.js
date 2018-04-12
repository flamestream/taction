import PropertyItemContent from '@/components/PropertyItemContent';
import PropertyItemColor from '@/components/PropertyItemColor';
import PropertyItemRectangle from '@/components/PropertyItemRectangle';
import PropertyItemType from '@/components/PropertyItemType';
import InputAsset from '@/components/InputAsset';
import InputOption from '@/components/InputOption';
import InputNumber from '@/components/InputNumber';
import InputText from '@/components/InputText';
import TextStyleType from '@/types/TextStyleType';

const fontWeightOptions = TextStyleType.prototype.$definition['font-weight'].data.options;

const component = [{
	label: 'Type',
	path: 'type',
	type: PropertyItemType,
	required: true
}, {
	label: 'Size',
	path: 'size',
	type: InputNumber,
	required: true,
	options: {
		min: 1,
		step: 1
	}
}, {
	label: 'Command',
	path: 'command',
	type: InputText,
	required: true
}, {
	label: 'Content',
	path: 'style.base.content',
	type: PropertyItemContent
}, {
	label: 'Font family',
	path: 'style.base.text-style.font-family',
	type: InputAsset,
	options: {
		prefix: './',
		exts: 'ttf'
	}
}, {
	label: 'Font size',
	path: 'style.base.text-style.font-size',
	type: InputNumber,
	options: {
		min: 1,
		step: 1
	}
}, {
	label: 'Font weight',
	path: 'style.base.text-style.font-weight',
	type: InputOption,
	options: {
		options: fontWeightOptions
	}
}, {
	label: 'Text color',
	path: 'style.base.text-style.color',
	type: PropertyItemColor
}, {
	label: 'Background color',
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
	label: 'Border thickness',
	path: 'style.base.border.thickness',
	type: PropertyItemRectangle
}, {
	label: 'Border roundness',
	path: 'style.base.border.radius',
	type: PropertyItemRectangle,
	options: { corner: true }
}, {
	label: 'Border color',
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
	label: 'Active content',
	path: 'style.active.content',
	type: PropertyItemContent
}, {
	label: 'Active font size',
	path: 'style.active.text-style.font-size',
	type: InputNumber,
	options: {
		min: 1,
		step: 1
	}
}, {
	label: 'Active font family',
	path: 'style.active.text-style.font-family',
	type: InputAsset,
	options: {
		prefix: './',
		exts: 'ttf'
	}
}, {
	label: 'Active font weight',
	path: 'style.active.text-style.font-weight',
	type: InputOption,
	options: {
		options: fontWeightOptions
	}
}, {
	label: 'Active text color',
	path: 'style.active.text-style.color',
	type: PropertyItemColor
}, {
	label: 'Active background color',
	path: 'style.active.color',
	type: PropertyItemColor
}, {
	label: 'Active margin',
	path: 'style.active.margin',
	type: PropertyItemRectangle
}, {
	label: 'Active padding',
	path: 'style.active.padding',
	type: PropertyItemRectangle
}, {
	label: 'Active border thickness',
	path: 'style.active.border.thickness',
	type: PropertyItemRectangle
}, {
	label: 'Active border roundness',
	path: 'style.active.border.radius',
	type: PropertyItemRectangle,
	options: { corner: true }
}, {
	label: 'Active border color',
	path: 'style.active.border.color',
	type: PropertyItemColor
}, {
	label: 'Active opacity',
	path: 'style.active.opacity',
	type: InputNumber,
	options: {
		min: 0,
		max: 1,
		step: 0.001
	}
}];

export default component;
