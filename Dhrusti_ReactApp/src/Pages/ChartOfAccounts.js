import React, { useEffect, useState } from "react";
import { GetLevelTypeAsync } from "../config/axiosCalls";
import LevelCRUD from "../components/level_crud.component";

const ChartsOfAccounts = () => {

    const [drpLevel1Data, setDrpLevel1Data] = useState([]);
    const [drpLevel2Data, setDrpLevel2Data] = useState([]);
    const [drpLevel3Data, setDrpLevel3Data] = useState([]);
    const [drpLevel4Data, setDrpLevel4Data] = useState([]);
    const [level2TableData, setLevel2TableData] = useState();
    const [level3TableData, setLevel3TableData] = useState();
    const [level4TableData, setLevel4TableData] = useState();
    const [level5TableData, setLevel5TableData] = useState();
    const [selectedLevel, setSelectedLevel] = useState("");
    const [previosSelectedLevel, setPreviousSelectedLevel] = useState("");

    useEffect(() => {
        GetDrpLevelData(1);
    }, []);

    const GetDrpLevelData = async (level) => {
        const response = await GetLevelTypeAsync({ parentLevelTypeId: previosSelectedLevel, levelId: level });

        setSelectedLevel(level);

        if (response.status === 200 && response.data.status) {
            if (level === 1) {
                setDrpLevel1Data(response.data.data.allLevelList);
            } else if (level === 2) {
                setDrpLevel2Data(response.data.data.allLevelList);
            } else if (level === 3) {
                setDrpLevel3Data(response.data.data.allLevelList);
            } else if (level === 4) {
                setDrpLevel4Data(response.data.data.allLevelList);
            }
        }
    }

    const OnDrpLevelOneChange = async (e) => {
        const option = e.target.selectedOptions[0].value;

        setPreviousSelectedLevel(option);
        if (option !== null) {
            var level = selectedLevel + 1;

            if (level === 2) {
                setDrpLevel2Data([]);
                setDrpLevel3Data([]);
                setDrpLevel4Data([]);
                setLevel2TableData();
                setLevel3TableData();
                setLevel4TableData();
            } else if (level === 3) {
                setDrpLevel3Data([]);
                setDrpLevel4Data([]);
                setLevel3TableData();
                setLevel4TableData();
            } else if (level === 4) {
                setDrpLevel4Data([]);
                setLevel4TableData();
            }
            setLevel5TableData();

            const response = await GetLevelTypeAsync({ parentLevelTypeId: option, levelId: level });

            if (response.status === 200 && response.data.status) {
                if (level === 2) {
                    setLevel2TableData(response.data.data.allLevelList);
                } else if (level === 3) {
                    setLevel3TableData(response.data.data.allLevelList);
                } else if (level === 4) {
                    setLevel4TableData(response.data.data.allLevelList);
                } else if (level === 5) {
                    setLevel5TableData(response.data.data.allLevelList);
                }
            }
        }
    }

    var heading = [{ name: 'Id', selector: row => row.id, sortable: true },
    { name: 'Code', selector: row => row.code, sortable: true },
    { name: 'Name', selector: row => row.name, sortable: true }];

    const selectedRowsChange = (e) => {
        if (e.selectedRows[0].id != null) {
            setPreviousSelectedLevel(e.selectedRows[0].id);
        }
    }

    return (
        <div className="card m-3 p-3">
            <form>
                <div className="mt-4">
                    <ul className="nav nav-tabs" id="myTab" role="tablist">
                        <li className="nav-item" role="presentation">
                            <button className="nav-link active" id="level2-tab" data-bs-toggle="tab" data-bs-target="#level2" type="button" role="tab" aria-controls="level2" aria-selected="true" onClick={() => GetDrpLevelData(1)}>Level 2</button>
                        </li>
                        <li className="nav-item" role="presentation">
                            <button className="nav-link" id="level3-tab" data-bs-toggle="tab" data-bs-target="#level3" type="button" role="tab" aria-controls="level3" aria-selected="false" onClick={() => GetDrpLevelData(2)}>Level 3</button>
                        </li>
                        <li className="nav-item" role="presentation">
                            <button className="nav-link" id="level4-tab" data-bs-toggle="tab" data-bs-target="#level4" type="button" role="tab" aria-controls="level4" aria-selected="false" onClick={() => GetDrpLevelData(3)}>Level 4</button>
                        </li>
                        <li className="nav-item" role="presentation">
                            <button className="nav-link" id="level5-tab" data-bs-toggle="tab" data-bs-target="#level5" type="button" role="tab" aria-controls="level5" aria-selected="false" onClick={() => GetDrpLevelData(4)}>Level 5</button>
                        </li>
                    </ul>
                    <div className="tab-content" id="myTabContent">
                        <div className="tab-pane fade show active" id="level2" role="tabpanel" aria-labelledby="level2-tab">
                            <LevelCRUD drpLevelData={drpLevel1Data} selectTitle="Select Level 1" setOnDrpChange={OnDrpLevelOneChange} heading={heading} levelTableData={level2TableData} selectedRowsChange={selectedRowsChange} is2ndLevel={true} />
                        </div>
                        <div className="tab-pane fade" id="level3" role="tabpanel" aria-labelledby="level3-tab">
                            <LevelCRUD drpLevelData={drpLevel2Data} selectTitle="Select Level 2" setOnDrpChange={OnDrpLevelOneChange} heading={heading} levelTableData={level3TableData} selectedRowsChange={selectedRowsChange} is2ndLevel={false}/>
                        </div>
                        <div className="tab-pane fade" id="level4" role="tabpanel" aria-labelledby="level4-tab">
                            <LevelCRUD drpLevelData={drpLevel3Data} selectTitle="Select Level 3" setOnDrpChange={OnDrpLevelOneChange} heading={heading} levelTableData={level4TableData} selectedRowsChange={selectedRowsChange} is2ndLevel={false}/>
                        </div>
                        <div className="tab-pane fade" id="level5" role="tabpanel" aria-labelledby="level5-tab">
                            <LevelCRUD drpLevelData={drpLevel4Data} selectTitle="Select Level 4" setOnDrpChange={OnDrpLevelOneChange} heading={heading} levelTableData={level5TableData} selectedRowsChange={selectedRowsChange} is2ndLevel={false}/>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )

}

export default ChartsOfAccounts;