import React from 'react';
import {ModelBtn, ModelBtnBar} from './ModelTemplate';

class ErrorContent extends React.Component {
  render() {
    return (
      <div>
        {this.props.children}
        <ModelBtnBar>
          <ModelBtn>好</ModelBtn>
        </ModelBtnBar>
      </div>
    );
  }
}

/**
 * @param title {string}
 * @param msg {string|JSX.Element}
 * @returns {{title: string, content: JSX.Element}}
 * @constructor
 */
function ErrorModel(title='发生错误', msg = '发生未知错误') {
  if (typeof msg === 'string') {
    msg = (<p>{msg}</p>);
  }
  return {
    title: title,
    content: (
      <ErrorContent children={msg}/>
    ),
  };
}

export default ErrorModel;
