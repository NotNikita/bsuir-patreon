import React from "react";
import { UserProfile } from "./profile.component";
import "./profile.styles.css";

import { Backdrop, Box, Modal, Fade, Button } from '@material-ui/core';
import FormInput from "../form-input.component";


const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

const UserDetails: React.FC<{ userProp: UserProfile, onChangePass: (oldPass: string, newPass: string) => void }> = ({ userProp, onChangePass }) => {
    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const [information, setInformation] = React.useState<UserProfile>({
        id: 'default',
        name: 'temp',
        surname: 'temp',
        balance: 0,
        subscriptions: [],
        followers: [],
        userName: 'username',
        email: 'email@mail.com',
        phoneNumber: '+123',
        lockoutEnabled: false
    })
    React.useEffect(() => {
        setInformation({
            ...userProp
        })
    }, [userProp])

    const [passwords, setPasswords] = React.useState({
        oldPassword: '',
        newPassword: ''
    })
    const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
        const { name, value } = event.currentTarget
        setPasswords({
            ...passwords,
            [name]: value
        })
    }
    const localSubmit = (event: React.SyntheticEvent) => {
        if (event) event.preventDefault();
        onChangePass(passwords.oldPassword, passwords.newPassword);
        setPasswords({
            oldPassword: '',
            newPassword: ''
        })
    }

    return (
        <div className="box">
            <div className="col px-2 py-2">
                <div className="row px-4 py-3 ">
                    <div className="row img pl-3">
                        <div className="bg-black shadow rounded overflow-hidden">
                            <div className="">
                                <div className="px-3 pt-2 pb-5 mb-1 cover">
                                    <span className="displayName">{information.userName}</span>
                                    <div className="row media align-items-end profile-head">
                                        <div className="col-3 profile display picture">
                                            <img
                                                src="https://www.placecage.com/500/500"
                                                alt="..."
                                                width="200"
                                                className="rounded mb-3 img-thumbnail"
                                            />
                                        </div>
                                        <div className="col-2 pt-1 pb-4 ml-0 username">
                                            <div className="name text-white">
                                                <span className="mt-0 mb-0 ml-0 h4 text-nowrap">{`${information.name} ${information.surname}`}</span>
                                                <p className="small mb-4 text-nowrap">
                                                    <a
                                                        href={`mailto: ${information.email}`}
                                                        className="text-white"
                                                    >
                                                        email
                                                    </a>
                                                    {" : "}
                                                    {information.email}
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="mb-4 bg-light d-flex justify-content-between align-content-center text-center">
                                <ul className="list-inline mb-1">
                                    <li className="list-inline-item px-5">
                                        <Button onClick={handleOpen}>Change password</Button>
                                        <Modal
                                            aria-labelledby="transition-modal-title"
                                            aria-describedby="transition-modal-description"
                                            open={open}
                                            onClose={handleClose}
                                            closeAfterTransition
                                            BackdropComponent={Backdrop}
                                            BackdropProps={{
                                                timeout: 500,
                                            }}
                                        >
                                            <Fade in={open}>
                                                <Box sx={style}>
                                                    <form onSubmit={localSubmit} className="sign-up-form">
                                                        <span className="d-flex justify-content-center">Change My Password</span>
                                                        <FormInput
                                                            type='text'
                                                            name='oldPassword'
                                                            value={passwords.oldPassword}
                                                            handleChange={handleChange}
                                                            label='Old Password'
                                                            required
                                                        />
                                                        <FormInput
                                                            type='text'
                                                            name='newPassword'
                                                            value={passwords.newPassword}
                                                            handleChange={handleChange}
                                                            label='New Password'
                                                            required
                                                        />
                                                        <Button type='submit'>Change Password!</Button>
                                                    </form>
                                                </Box>
                                            </Fade>
                                        </Modal>
                                    </li>
                                </ul>
                                <ul className="list-inline mb-1">
                                    <li className="list-inline-item px-5">
                                        <h5 className="font-weight-bold mb-0 d-block">
                                            {`${information.balance || 0} $`}
                                        </h5>
                                        <small className="text-muted">
                                            <i className="fas mr-1"></i>Balance
                                        </small>
                                    </li>
                                    <li className="list-inline-item py-2 px-2">
                                        <h5 className="font-weight-bold mb-0 d-block">
                                            {information.followers ? information.followers.length : 0}
                                        </h5>
                                        <small className="text-muted">
                                            <i className="fas mr-2"></i>Followers
                                        </small>
                                    </li>
                                    <li className="list-inline-item py-2 px-2">
                                        <h5 className="font-weight-bold mb-0 d-block">
                                            {information.subscriptions ? information.subscriptions.length : 0}
                                        </h5>
                                        <small className="text-muted">
                                            <i className="fas mr-2"></i>Subscriptions
                                        </small>
                                    </li>
                                </ul>
                            </div>

                            <div className="px-4">
                                <h5 className="mb-0">Subscriptions</h5>
                                <div className="p-4 rounded shadow-sm bg-light">
                                    <p className="font-italic mb-0">Here will be list of Subscriptions in circles</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default UserDetails;
