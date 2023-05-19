import Table from "./table.component";
import Select from "./select.component";
import ModalPopup from "./modal.component";

const LevelCRUD = (props) => {

    var drpLevelData = props.drpLevelData;
    var selectTitle = props.selectTitle;
    var heading = props.heading;
    var levelTableData = props.levelTableData;
    var is2ndLevel = props.is2ndLevel;

    return (
        <div>
            <div className="row mt-3">
                <div className="col-md-2">
                    <Select data={drpLevelData} selectTitle={selectTitle} setOnDrpChange={props.setOnDrpChange} />
                </div>
                <div className="col-md-10">
                    <ModalPopup is2ndLevel={is2ndLevel}/>
                </div>
            </div>
            <Table heading={heading} body={levelTableData} selectedRowsChange={props.selectedRowsChange} />
        </div>
    )
}

export default LevelCRUD;