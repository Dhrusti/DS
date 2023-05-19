import React, { useState } from 'react'
import { Modal, Button, Form, Dropdown } from 'react-bootstrap'
import { AddAllLevelAsync, GenerateTypeCodeAsync } from "../config/axiosCalls";
import DropdownItem from 'react-bootstrap/esm/DropdownItem';
import { toast } from 'react-toastify';

const ModalPopup = (props) => {

    const [show, setShow] = useState(false);
    const [code, setCode] = useState("");

    const handleClose = () => setShow(false);
    const handleShow = async () => {
        const response = await GenerateTypeCodeAsync({ parentId: "64476c38d06b6624c8fa3165", levelId: 2 });
        if (response.status === 200 && response.data.status) {
            setCode(response.data.data.code);
            setShow(true);
        }
    };
    const handleSaveChanges = async () => {
        const response = await AddAllLevelAsync({
            code: code,
            name: "Income Lv 2",
            parentLevelTypeId: "64476c38d06b6624c8fa3165",
            isFinalLevel: false,
            creditOrDebit: "",
            levelId: 2
        });
        if (response.status === 200 && response.data.status) {
            toast(response.data.message);
            setShow(false);
        }
    }

    var is2ndLevel = props.is2ndLevel;

    return (
        <>
            <Button variant="primary" className='btn btn-primary float-end' onClick={handleShow}>
                Add Level
            </Button>
            <Modal show={show} onHide={handleClose} backdrop="static" keyboard={false}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Assets</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3" controlId="code">
                            <Form.Label>Code</Form.Label>
                            <Form.Control placeholder="Code" value={code} readOnly />
                            <Form.Text className="text-muted">
                                Code will be auto-generated, If it is not present then refresh the page.
                            </Form.Text>
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="name">
                            <Form.Label>Name</Form.Label>
                            <Form.Control type="text" placeholder="Enter Name" />
                        </Form.Group>
                        {!is2ndLevel ?
                            <div>
                                <Form.Group className="mb-3" controlId="isFinalLevel">
                                    <Form.Check type="checkbox" label="Is it a final level?" />
                                </Form.Group>
                                <Form.Select>
                                    <option defaultChecked>Select</option>
                                    <option>Credit</option>
                                    <option>Debit</option>
                                </Form.Select>
                            </div> : <div />}
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleSaveChanges}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}

export default ModalPopup;