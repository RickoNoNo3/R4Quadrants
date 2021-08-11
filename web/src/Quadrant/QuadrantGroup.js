import React from 'react';
import Quadrant from './Quadrant';
import './Quadrant.css';

class QuadrantGroup extends React.Component {
	render() {
		return (
			<div className="QuadrantGroup">
				<Quadrant color="#774444" index={0}/>
				<Quadrant color="#AAAA44" index={1}/>
				<Quadrant color="#444444" index={2}/>
				<Quadrant color="#443377" index={3}/>
			</div>
		);
	}
}

export default QuadrantGroup;