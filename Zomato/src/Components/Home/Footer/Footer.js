import React from 'react'
import footer from '../../../Assest/footer.avif';
import gpay from '../../../Assest/gpay.webp';
import app from '../../../Assest/appstore.webp';
import './Footer.css'
const Footer = () => {
  return (
    <>
            <div className="Footer">
                <div className="container">
                <img src={footer} alt="Food image" className='footerlogo' />
                    <div className="Footer-menu">
                        <div className="Footer-contain">
                            <span>About Zomato</span>
                            <p>Who We Are</p>
                            <p>Blog</p>
                            <p>Work With Us</p>
                            <p>Investor Relations</p>
                            <p>Report Fraud</p>
                            <p>Contact Us</p>
                        </div>
                        <div className="Footer-contain">
                            <span>Zomaverse</span>
                            <p>Zomato</p>
                            <p>Feeding India</p>
                            <p>Hyperpure</p>
                            <p>Zomaland</p>
                        </div>
                        <div className="Footer-contain">
                            <span>Partner With Us</span>
                            <p>Apps For You</p>
                            <div className="Footer-contain Footer-part ">
                                <span>For Enterprises</span>
                                <p>Zomato For Work</p>
                            </div>
                        </div>
                        <div className="Footer-contain">
                            <span>Learn More</span>
                            <p>Privacy</p>
                            <p>Security</p>
                            <p>Terms</p>
                            <p>Sitemap</p>

                        </div>
                        <div className="Footer-contain">
                            <span>Social links</span>
                            <p>
                            <a className="footer-social" href="https://www.youtube.com/" target="_blank"
                                    title="Visit on Linkdin"><i class="fa-brands fa-linkedin"></i></a>
                            
                            <a className="footer-social" href="https://www.instagram.com/" target="_blank"
                                    title="Visit on Instagram"><i className="fa-brands fa-instagram"></i></a> 
                                    
                                     <a className="footer-social" href="https://mobile.twitter.com/login" target="_blank"
                                    title="Visit on Twitter"><i className="fa-brands fa-twitter"></i></a>

                               <a className="footer-social" href="https://www.facebook.com/" target="_blank"
                                    title="Visit on Facebook"><i className="fa-brands fa-facebook"></i></a>
                               
                                <a className="footer-social" href="https://www.youtube.com/" target="_blank"
                                    title="Visit on Youtube"><i className="fa-brands fa-youtube"></i></a>
                            </p>
                            <div className="footer-img">
                            <img src={gpay} alt="Food image" />
                            <img src={app} alt="Food image" />
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div className="copyrights">
                        <p>By continuing past this page, you agree to our Terms of Service, Cookie Policy, Privacy Policy and Content Policies. All trademarks are properties of their respective owners. 2008-2022 © Zomato™ Ltd. All rights reserved.</p>
                    </div>
                </div>
            </div>


        </>
  )
}

export default Footer
