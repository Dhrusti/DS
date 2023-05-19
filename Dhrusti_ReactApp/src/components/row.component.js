const TableRow = (props) => {
    var row = props.rowContent;
    return (
        <tr>
            {row.map((val, rowID) => <td key={rowID}>{val}</td>)}
        </tr>
    )
}

export default TableRow;