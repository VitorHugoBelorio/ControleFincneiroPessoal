import React from 'react';

const Button = ({ children, onClick, type = "button", className = "" }) => {
    return (
        <button type={type} onClick={onClick} className={`btn btn-primary w-100 ${className}`}>
            {children}
        </button>
    );
};

export default Button;
