import React from 'react';


const LoadingCircle: React.FC = () => {
  return (
    <div className="flex">
        <div className="bg-gray-400 p-2 rounded-full ">
            <div className="border-5 border-t-5 border-gray-200 border-t-white w-8 h-8 rounded-full animate-spin" />
        </div>
    </div>
  );
};

export default LoadingCircle;
