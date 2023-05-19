import React from "react";
import App from "../App";
import "../App.css";

export const CalculateDistance = () => {
  return (
    <>
     <div className="container">
      <div className="mainForm">
        <div className="childform">
          <form action="">
            <div class="form-outline">
              <div className="formLable">
                <label class="form-label" for="typeNumber">
                  Enter Sum Zip Code :
                </label>
              </div>
              <div className="formInput">
                <input
                  type="number"
                  id="typeNumber"
                  name="sumZip"
                  class="form-control"
                  // onChange={(e) => handleChange(e)}
                />
              </div>
            </div>
            <div class="form-outline">
              <div className="formLable">
                <label class="form-label" for="typeNumber">
                  Enter Destination Zip Code :
                </label>
              </div>
              <div className="formInput">
                <input
                  type="number"
                  id="typeNumber"
                  name="destinationZip"
                  class="form-control"
                  // onChange={(e) => handleChange(e)}
                />
              </div>
            </div>
            <div className="text-center mt-4">
              <button
                type="button"
                class="btn btn-primary"
                // onClick={handleClick}
              >
                Submit
              </button>
              <button type="button" class="btn btn-danger">
                Reset
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
    </>
  );
};
