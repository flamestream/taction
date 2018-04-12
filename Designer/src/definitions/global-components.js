import { cloneDeepWith } from 'lodash';
import PropertyItemColor from '@/components/PropertyItemColor';
import PropertyItemRectangle from '@/components/PropertyItemRectangle';
import InputOption from '@/components/InputOption';
import InputNumber from '@/components/InputNumber';
import InputText from '@/components/InputText';
import InputBoolean from '@/components/InputBoolean';
import LayoutType from '@/types/LayoutType';
import itemComponentDefinitions from './item-components';

const globalButtonStylePrefix = 'default-button-'
const orientationOptions = LayoutType.prototype.$definition.orientation.data.options;

let components = [{
	label: 'Name',
	path: 'name',
	type: InputText,
	required: true
}, {
	label: 'Orientation',
	path: 'orientation',
	type: InputOption,
	required: true,
	options: {
		options: orientationOptions
	}
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
	label: 'Background color',
	path: 'color',
	type: PropertyItemColor
}, {
	label: 'Margin',
	path: 'margin',
	type: PropertyItemRectangle
}, {
	label: 'Border thickness',
	path: 'border.thickness',
	type: PropertyItemRectangle
}, {
	label: 'Border roundness',
	path: 'border.radius',
	type: PropertyItemRectangle,
	options: { corner: true }
}, {
	label: 'Border color',
	path: 'border.color',
	type: PropertyItemColor
}, {
	label: 'Opacity',
	path: 'opacity',
	type: InputNumber,
	options: {
		min: 0,
		max: 1,
		step: 0.001
	}
}, {
	label: 'Opacity when hidden',
	path: 'opacity-hide',
	type: InputNumber,
	options: {
		min: 0,
		max: 1,
		step: 0.001
	}
}, {
	label: 'Hide on mouse-over',
	path: 'disable-hide',
	type: InputBoolean,
	options: {
		invert: true
	}
}, {
	label: 'Fade out animation',
	path: 'disable-fade-animation',
	type: InputBoolean,
	options: {
		invert: true
	}
}, {
	label: 'Radial menu animation',
	path: 'disable-radial-menu-animation',
	type: InputBoolean,
	options: {
		invert: true
	}
}];

// Add global item properties;
let globalItemComponentDefinitions = cloneDeepWith(itemComponentDefinitions, o => {
	// Assumed this is enough to detect vue definition
	if (o.name && o.components) {
		return o;
	}
});
globalItemComponentDefinitions = globalItemComponentDefinitions.filter(c => c.path.startsWith('style.'));
globalItemComponentDefinitions = globalItemComponentDefinitions.map(c => {
	c.path = globalButtonStylePrefix + c.path;
	c.label = 'Default button ' + c.label.toLowerCase();
	return c;
});

components = components.concat(globalItemComponentDefinitions);

export default components;
