import React from 'react';
import './WidgetTotal.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

export default function WidgetTotal({obj}) {
  const {icon, value, label, className} = obj;
  return (
    <div className={`widget ${className}`}>
      <FontAwesomeIcon className="icon" icon={icon} />
      <h4>{value}</h4>
      <strong>{label}</strong>
    </div>
  );
}

