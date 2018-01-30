// Modified from https://gist.github.com/gre/296291b8ce0d8fe6e1c3ea4f1d1c5c3b
// more minimal version of https://github.com/olahol/scrollparent.js/blob/master/scrollparent.js
const regex = /(auto|scroll)/;

const style = (node, prop) =>
	getComputedStyle(node, null).getPropertyValue(prop);

const scroll = (node) =>
	regex.test(
		style(node, 'overflow') +
		style(node, 'overflow-y') +
		style(node, 'overflow-x'));

const scrollparent = (node) =>
	!node || node === document.body
		? document.body
		: scroll(node)
			? node
			: scrollparent(node.parentNode);

export default scrollparent;
