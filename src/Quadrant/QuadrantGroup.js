import React from 'react';
import Quadrant from './Quadrant';
import './Quadrant.css';

class QuadrantGroup extends React.Component {
	render() {
		return (
			<div className="QuadrantGroup">
				<Quadrant color="#774444"/>
				<Quadrant color="#AAAA44"/>
				<Quadrant color="#444444"/>
				<Quadrant color="#443377"/>
			</div>
		);
	}
}

export default QuadrantGroup;