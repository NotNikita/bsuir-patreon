import React from "react";
import { UserProfile } from "./profile.component";
import "./profile.styles.css";


const UserDetails: React.FC<{ userProp: UserProfile }> = ({ userProp }) => {
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

                            <div className="mb-4 bg-light d-flex justify-content-end text-center">
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
                                <h5 className="mb-0">Description</h5>
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
