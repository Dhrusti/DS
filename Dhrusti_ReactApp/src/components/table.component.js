import {useState} from "react";
import DataTable from "react-data-table-component";
import TableRow from "./row.component";

const Table = (props) => {
    var heading = props.heading;
    var body = props.body;

    return (
        <div className="mt-2">
            <DataTable
                columns={heading}
                data={body}
                pagination
                selectableRowsHighlight
                fixedHeader>
            </DataTable>
        </div>
        // <table style={{ width: "800px", border: "2px solid forestgreen" }}>
        //     <tr>
        //         {heading.map((head, headID) => <th style={{borderBottom:"1px solid black"}} key={headID} >{head}</th>)}
        //     </tr>
        //     {body.map((rowContent, rowID) => {
        //         return (
        //             <tr key={rowID}>
        //                 <td>{rowContent.id}</td>
        //                 <td>{rowContent.code}</td>
        //                 <td>{rowContent.name}</td>
        //             </tr>
        //         )
        //     })}
        //</table>
        // <table style={{ width: 500 }}>
        //     <thead>
        //         <tr>
        //             {heading.map((head, headID) => <th key={headID} >{head}</th>)}
        //         </tr>
        //     </thead>
        //     <tbody>
        //         {body.map((rowContent, rowID) => <TableRow rowContent={rowContent} key={rowID} />)}
        //     </tbody>
        // </table>
    );
}

export default Table;